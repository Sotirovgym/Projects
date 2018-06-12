namespace HTTPServer
{
    using ByTheCake;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    class Launcher : IRunnable
    {
        static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new ByTheCakeApplication();
            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(1337, appRouteConfig);

            webServer.Run();
        }
    }
}
