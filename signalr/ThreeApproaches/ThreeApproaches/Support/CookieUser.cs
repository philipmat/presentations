using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeApproaches.Support
{
    public class CookieUser : IUserIdProvider
    {
        public string GetUserId(IRequest request) {
            if (request.Cookies.ContainsKey("username"))
                return request.Cookies["username"].Value;
            return string.Empty;
        }
    }
}