using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
                var curso = await context.Curso.FindAsync(request.Id);

                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = $"No se encontró el curso {request.Id}" });
                }

                // * Se eliminan los instructores relacionados con el curso
                var listaCursoInstructor = await context.CursoInstructor.Where(x => x.CursoId == curso.CursoId).ToListAsync();
                foreach (var cursoInstructor in listaCursoInstructor)
                {
                    context.CursoInstructor.Remove(cursoInstructor);
                }

                // * Se eliminan los precios relacionados al curso
                var precio = await context.Precio.Where(x => x.CursoId == curso.CursoId).FirstOrDefaultAsync();
                if (precio != null)
                {
                    context.Precio.Remove(precio);
                }

                // * Se eliminan los comentarios relacionados al curso
                var listaComentario = await context.Comentario.Where(x => x.CursoId == curso.CursoId).ToListAsync();
                foreach (var comentario in listaComentario)
                {
                    context.Comentario.Remove(comentario);
                }

                // * Se elimina el curso
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