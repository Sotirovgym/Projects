namespace HTTPServer.Server.Http.Response
{
    using HTTPServer.Server.Common;
    using HTTPServer.Server.Enums;
    using System;

    public class InternalServerErrorResponse : ViewResponse
    {
        public InternalServerErrorResponse(Exception ex)
            : base(HttpStatusCode.InternalServerError, new InternalServerErrorView(ex))
        {
        }
    }
}
