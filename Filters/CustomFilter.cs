using Microsoft.AspNetCore.Mvc.Filters;

namespace Tennis.API.Filters
{
    public class CustomFilter : IAsyncActionFilter
    {
        private readonly ILogger<CustomFilter> _logger;

        public CustomFilter(ILogger<CustomFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Lamada desde el request");
            await next();
            _logger.LogInformation("Lamada desde el response");
        }
    }
}
