using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Comentarios
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }

        public class ManejarEjecuta : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext context;

            public ManejarEjecuta(CursosOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var comentario = await context.Comentario.FindAsync(request.Id);
                if (comentario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encotró el comentario a borrar" });
                }

                context.Comentario.Remove(comentario);

                var resultado = await context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo eliminar el comentario");
            }
        }
    }
}