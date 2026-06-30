using DientesLimpios.Aplicacion.Excepciones;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace DientesLimpios.API.Middlewares
{
    public class ManejadorExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;
        public ManejadorExcepcionesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManejarExcepcionAsync(context, ex);
            }
        }

        private Task ManejarExcepcionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            var resultado = string.Empty;

            switch (exception)
            {
                default:
                    break;
                case ExcepcionNoEncontrado:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ExcepcionDeValidacion:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    var excepcionDeValidacion = exception as ExcepcionDeValidacion;
                    resultado = JsonSerializer.Serialize(excepcionDeValidacion?.ErroresDeValidacion);
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(resultado);
        }

    }

    public static class ManejadorExcepcionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseManejadorExcepcionesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManejadorExcepcionesMiddleware>();
        }
    }
}
