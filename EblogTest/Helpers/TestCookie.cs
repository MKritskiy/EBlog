﻿using EBlog.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EblogTest.Helpers
{
    internal class TestCookie : IWebCookie
    {
        Dictionary<string, string> cookies = new Dictionary<string, string>();

        public void Add(string cookieName, string cookieValue, int days = 0)
        {
            cookies.Add(cookieName, cookieValue);
        }

        public void AddSecure(string cookieName, string cookieValue, int days = 0)
        {
            cookies.Add(cookieName, cookieValue);
        }

        public void Delete(string cookieName)
        {
            cookies.Remove(cookieName);
        }

        public string? Get(string cookieName)
        {
            if (cookies.ContainsKey(cookieName)) 
                return cookies[cookieName];
            return null;
        }

        public void Clear()
        {
            cookies.Clear();
        }
    }
}
