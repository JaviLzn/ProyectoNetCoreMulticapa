using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Comentarios
{
    public class Nuevo
    {
        public class CrearComentario : IRequest
        {
            public string Alumno { get; set; }
            public int Puntaje { get; set; }
            public string Comentario { get; set; }
            public Guid CursoId { get; set; }
        }

        public class ValidarCrearComentario : AbstractValidator<CrearComentario>
        {
            public ValidarCrearComentario()
            {
                RuleFor(X => X.Alumno).NotEmpty();
                RuleFor(X => X.Puntaje).NotEmpty();
                RuleFor(X => X.Comentario).NotEmpty();
                RuleFor(X => X.CursoId).NotEmpty();
            }
        }

        public class ManejarCrearComentario : IRequestHandler<CrearComentario>
        {
            private readonly CursosOnlineContext context;

            public ManejarCrearComentario(CursosOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(CrearComentario request, CancellationToken cancellationToken)
            {
                var comentario = new Comentario{
                    ComentarioId = Guid.NewGuid(),
                    Alumno = request.Alumno,
                    ComentarioTexto = request.Comentario,
                    Puntaje = request.Puntaje,
                    CursoId = request.CursoId
                };

                context.Comentario.Add(comentario);
                var resultado = await context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo crear el comentario");
            }
        }


    }
}