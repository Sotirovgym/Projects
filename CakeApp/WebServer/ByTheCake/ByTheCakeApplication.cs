namespace HTTPServer.ByTheCake
{
    using HTTPServer.ByTheCake.Controllers;
    using HTTPServer.Server.Contracts;
    using HTTPServer.Server.Routing.Contracts;

    public class ByTheCakeApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
                "/",
                req => new HomeController().Index());

            appRouteConfig.Get(
                "About",
                req => new HomeController().About());

            appRouteConfig.Get(
                "/Add",
                req => new CakesController().Add());

            appRouteConfig.Post(
                "/Add",
                req => new CakesController().Add(req.FormData["name"], req.FormData["price"]));

            appRouteConfig.Get(
                "/Search",
                req => new CakesController().Search(req));

            appRouteConfig.Get(
                "/Shopping/Add/{(?<id>[0-9]+)}",
                req => new ShoppingController().AddToCard(req));

            appRouteConfig.Get(
                "/Cart",
                req => new ShoppingController().ShowCart(req));

            appRouteConfig.Post(
                "Shopping/Finish-order",
                req => new ShoppingController().FinishOrder(req));

            appRouteConfig.Post(
                "/Logout",
                req => new AccountsController().Logout(req));

            appRouteConfig.Get(
                "/Login",
                req => new AccountsController().Login());

            appRouteConfig.Post(
                "/Login",
                req => new AccountsController().Login(req));
        }
    }
}
