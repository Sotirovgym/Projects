namespace HTTPServer.Server.Http.Response
{
    using Enums;
    using HTTPServer.Server.Common;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse()
            : base (HttpStatusCode.NotFound, new NotFoundVIew())
        {
        }
    }
}
