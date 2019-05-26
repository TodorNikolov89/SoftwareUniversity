﻿using SIS.HTTP.Common;
using SIS.HTTP.Cookies.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private Dictionary<string, HttpCookie> httpCookies;

        public HttpCookieCollection()
        {
            this.httpCookies = new Dictionary<string, HttpCookie>();
        }

        public void AddCookie(HttpCookie httpCookie)
        {
            CoreValidator.ThrowIfNull(httpCookie, nameof(httpCookie));

            this.httpCookies.Add(httpCookie.Key, httpCookie);
        }

        public bool ContainsCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return this.httpCookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            HttpCookie httpCookie = this.httpCookies[key];

            return httpCookie;
        }


        public bool HasCookies()
        {
            return this.httpCookies.Count != 0;
        }


        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.httpCookies.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var cookie in this.httpCookies.Values)
            {
                sb.Append($"Set-Cookie: {cookie}").Append(GlobalConstants.HttpNewLine);
            }

            return sb.ToString();
        }

    }
}

