
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System.IO;
using System.Runtime.CompilerServices;

namespace Demo.app.Controllers
{
    public abstract class BaseController
    {
        public IHttpResponse View([CallerMemberName] string view = "Home")
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File.ReadAllText("../../../Views/" + controllerName + "/" + viewName + ".html");

            HtmlResult htmlResult = new HtmlResult(viewContent, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);

            htmlResult.Cookies.AddCookie(new HttpCookie("Lang", "en"));
            return new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
        }
    }
}
