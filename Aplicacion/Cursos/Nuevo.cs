using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> ListaInstructor { get; set; }
            public decimal Precio { get; set; }
            public decimal PrecioPromocion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
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

                Guid cursoId = Guid.NewGuid();

                var curso = new Curso
                {
                    CursoId = cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow
                };

                context.Curso.Add(curso);

                if (request.ListaInstructor != null)
                {
                    foreach (var id in request.ListaInstructor)
                    {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = curso.CursoId,
                            InstructorId = id
                        };
                        context.CursoInstructor.Add(cursoInstructor);
                    }
                }

                /* Agregar logica para insertar un precio del curso*/

                Precio precio = new Precio {
                    CursoId = curso.CursoId,
                    PrecioActual = request.Precio,
                    Promocion  = request.PrecioPromocion,
                    PrecioId = Guid.NewGuid()
                };
                context.Precio.Add(precio);

                var valor = await context.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el curso");
            }
        }
    }


}