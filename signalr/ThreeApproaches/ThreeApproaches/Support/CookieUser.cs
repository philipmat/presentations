using Microsoft.AspNet.SignalR;

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