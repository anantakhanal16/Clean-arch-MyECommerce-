using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddlewares> _logger;

        public ExceptionHandlingMiddlewares(RequestDelegate next, ILogger<ExceptionHandlingMiddlewares> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;

            }
        }
        private void LogException(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
