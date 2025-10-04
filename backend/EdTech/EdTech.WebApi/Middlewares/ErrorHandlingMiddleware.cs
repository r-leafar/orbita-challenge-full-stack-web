using EdTech.Core.Exceptions;

namespace EdTech.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Erro no Dominio",
                    message = ex.Message,
                   innerMessage = ex.InnerException?.Message
                });
            }
            catch (EdTech.Application.Exceptions.ApplicationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Erro na Aplicação",
                    message = ex.Message,
                    innerMessage = ex.InnerException?.Message

                });
            }
            catch (EdTech.Infrastructure.Exceptions.InfraestructureException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Erro na Infraestrutura",
                    message = ex.Message,
                    innerMessage = ex.InnerException?.Message

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Erro inesperado",
                    message = "Um erro inesperado ocorreu. Por favor, tente novamente mais tarde.",
                    innerMessage = ex.InnerException?.Message
                });
            }
        }
    }

}
