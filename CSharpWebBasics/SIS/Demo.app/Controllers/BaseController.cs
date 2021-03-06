﻿
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Demo.app.Controllers
{
    public abstract class BaseController
    {
        protected IHttpRequest HttpRequest { get; set; }
        private string ParseTemplate(string viewContent)
        {
            if (this.IsLoggedIn())
            {
                return viewContent.Replace("@Model.HelloMessage", $"Hello, {this.HttpRequest.Session.GetParameter("username")}");
            }
            else
            {
                return viewContent.Replace("@Model.HelloMessage", "Hello World from SIS.WebServer");
            }
        }

        private bool IsLoggedIn()
        {
            return this.HttpRequest.Session.ContainsParameter("username");
        }

        public IHttpResponse View([CallerMemberName] string view = "Home")
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File.ReadAllText("../../../Views/" + controllerName + "/" + viewName + ".html");

            viewContent = this.ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            htmlResult.Cookies.AddCookie(new HttpCookie("Lang", "en"));
            return htmlResult;
        }

        public IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }
    }
}
