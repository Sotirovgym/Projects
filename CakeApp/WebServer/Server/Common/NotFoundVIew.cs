namespace HTTPServer.Server.Common
{
    using HTTPServer.Server.Contracts;

    public class NotFoundVIew : IView
    {
        public string View()
        {
            return "<h1>404 This page does not exist :/</h1>";
        }
    }
}
