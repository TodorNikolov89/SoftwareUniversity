﻿
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace Demo.app.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Home(IHttpRequest httpRequest)
        {
            return this.View();
        }
    }
}
