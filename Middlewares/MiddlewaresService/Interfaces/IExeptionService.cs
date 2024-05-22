using Tennis.API.Excepcion;

namespace Tennis.API.Middlewares.MiddlewaresService.Interfaces
{
    public interface IExceptionService
    {
        Task GetBadRequestExceptionResponseAsync(HttpContext context, BadRequestException badRequestException);
    }
}
