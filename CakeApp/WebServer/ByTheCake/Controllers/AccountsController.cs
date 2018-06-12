namespace HTTPServer.ByTheCake.Controllers
{
    using HTTPServer.ByTheCake.Helpers;
    using HTTPServer.ByTheCake.Models;
    using HTTPServer.Server.Http;
    using HTTPServer.Server.Http.Contracts;
    using HTTPServer.Server.Http.Response;

    public class AccountsController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"Accounts\Login");
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            const string formNameKey = "username";
            const string formPasswordKey = "password";

            if (!request.FormData.ContainsKey(formNameKey)
                || !request.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var name = request.FormData[formNameKey];
            var password = request.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = "The field should not be empty";
                this.ViewData["showError"] = "block";

                return this.FileViewResponse(@"Accounts\Login");
            }

            request.Session.Add(SessionStore.CurrentUserKey, name);
            request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/Login");
        }
    }
}
