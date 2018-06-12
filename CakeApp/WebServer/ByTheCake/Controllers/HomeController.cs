namespace HTTPServer.ByTheCake.Controllers
{
    using Server.Http.Contracts;
    using Helpers;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
        {
            return this.FileViewResponse("Home/Index");
        }

        public IHttpResponse About()
        {
            return this.FileViewResponse("Home/About");
        }
    }
}
