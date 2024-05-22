using Tennis.API.Excepcion;
using Tennis.API.Middlewares.MiddlewaresService.Interfaces;
using System.Net;
using System.Text;

namespace Tennis.API.Middlewares.MiddlewaresService
{
    public class ExceptionService : IExceptionService
    {
        public async Task GetBadRequestExceptionResponseAsync(HttpContext context, BadRequestException badRequestException)
        {
            context.Response.ContentType = context.Response.ContentType == null ?
                "application/problem+json" :
                context.Response.ContentType + ";application/problem+json";

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var error = badRequestException.GetJsonDescription();

            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(error));
        }
    }
}
