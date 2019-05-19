using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;

namespace SIS.Demo
{
    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            string content = "<h1>Hello, World</h1>";

            return new HtmlResult(content, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);
        }
    }
}
