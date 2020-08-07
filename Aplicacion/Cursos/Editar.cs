using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> ListaInstructor { get; set; }
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
            private readonly IMapper mapper;

            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = $"No se encontró el curso {request.CursoId}" });
                }

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                if (request.ListaInstructor != null)
                {
                    if(request.ListaInstructor.Count > 0){
                        //Borrar las relaciones actuales
                        var ListCursoInstructor = context.CursoInstructor.Where(x => x.CursoId == request.CursoId);
                        foreach (var cursoInstructor in ListCursoInstructor)
                        {
                            context.CursoInstructor.Remove(cursoInstructor);
                        }
                        //Insertar las nuevas relaciones
                        foreach (var id in request.ListaInstructor)
                        {
                            context.CursoInstructor.Add(new CursoInstructor{
                                CursoId = request.CursoId,
                                InstructorId = id
                            });
                        }
                    } 
                }

                var valor = await context.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"No se pudo guardar los cambios el curso {request.CursoId}");

            }
        }
    }
}