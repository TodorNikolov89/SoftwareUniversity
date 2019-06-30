using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using SIS.WebServer.Routing.Contracts;
using SIS.WebServer.Sessions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;


        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));
            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public async Task ProcessRequestAsync()
        {
            IHttpResponse httpResponse = null;

            try
            {
                IHttpRequest httpRequest = await this.ReadRequestAsync();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");
                    string sessionId = this.SetRequestSession(httpRequest);
                    httpResponse = this.HandleRequest(httpRequest);
                    this.SetResponseSession(httpResponse, sessionId);
                    await this.PrepareResponseAsync(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                httpResponse = new TextResult(e.Message, HttpResponseStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await this.PrepareResponseAsync(httpResponse);
                httpResponse = new TextResult(e.Message, HttpResponseStatusCode.InternalServerError);
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequestAsync()
        {
            //Parse request from byte data
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }
            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());

        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            //execute function for current request -> returns response
            if (!this.serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path \"{httpRequest.Path}\" not fount.", HttpResponseStatusCode.NotFound);
            }

            return this.serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path).Invoke(httpRequest);

        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                httpResponse.Cookies.AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey, sessionId));
            }
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }

            return sessionId;
        }

        private async Task PrepareResponseAsync(IHttpResponse httpResponse)
        {
            //prepares response -> maps it to byte data

            byte[] byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);

        }
    }
}
