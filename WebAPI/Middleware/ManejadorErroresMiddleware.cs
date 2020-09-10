using Aplicacion.ManejadorError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ManejadorErroresMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ManejadorErroresMiddleware> logger;

        public ManejadorErroresMiddleware(RequestDelegate next, ILogger<ManejadorErroresMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ManejadorExcepcionAsync(context, ex, logger);
            }
        }

        private async Task ManejadorExcepcionAsync(HttpContext context, Exception ex, ILogger<ManejadorErroresMiddleware> logger)
        {
            object errores = null;

            switch (ex)
            {
                case ManejadorExcepcion me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me.Errores;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de Servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonSerializer.Serialize(new { errores });
                await context.Response.WriteAsync(resultados);
            }

        }
    }
}