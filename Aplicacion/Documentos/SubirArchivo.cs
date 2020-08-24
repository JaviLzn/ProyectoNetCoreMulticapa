using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Documentos
{
    public class SubirArchivo
    {
        public class Ejecuta : IRequest
        {
            public Guid? ObjectoReferencia { get; set; }
            public string Data { get; set; }
            public string Nombre { get; set; }
            public string Extension { get; set; }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext context;

            public Manejador(CursosOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var documento = await context.Documento.Where(x => x.ObjetoReferencia == request.ObjectoReferencia).FirstOrDefaultAsync();

                if (documento == null)
                {
                    var doc = new Documento
                    {
                        Contenido = Convert.FromBase64String(request.Data),
                        Nombre = request.Nombre,
                        Extension = request.Extension,
                        DocumentoId = Guid.NewGuid(),
                        FechaCreacion = DateTime.UtcNow,
                        ObjetoReferencia = request.ObjectoReferencia ?? Guid.Empty
                    };

                    context.Documento.Add(doc);
                }
                else
                {
                    documento.Contenido = Convert.FromBase64String(request.Data);
                    documento.Nombre = request.Nombre;
                    documento.Extension = request.Extension;
                    documento.FechaCreacion = DateTime.UtcNow;
                }

                var resultado = await context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar los cambios al subir archivo");

            }
        }
    }
}