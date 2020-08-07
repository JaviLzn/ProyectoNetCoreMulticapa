using System;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
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
                var listaCursoInstructor = context.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach (var cursoInstructor in listaCursoInstructor)
                {
                    context.CursoInstructor.Remove(cursoInstructor);
                }


                var curso = await context.Curso.FindAsync(request.Id);

                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = $"No se encontró el curso {request.Id}" });
                }

                context.Remove(curso);
                var resultado = await context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"No se pudo guardar eliminar el curso {request.Id}");


            }
        }

    }
}