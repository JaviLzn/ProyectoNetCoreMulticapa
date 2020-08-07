using System.Net;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;
using Aplicacion.ManejadorError;

namespace Aplicacion.Instructores
{
    public class Nuevo
    {
        public class CrearInstructor : IRequest {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string  Grado { get; set; }
        }

        public class ValidarCrearInstructor : AbstractValidator<CrearInstructor>
        {
            public ValidarCrearInstructor()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Grado).NotEmpty();
            }
        }

        public class ManejarCrearInstructor : IRequestHandler<CrearInstructor>
        {
            private readonly IInstructor instructor;
            public ManejarCrearInstructor(IInstructor instructor)
            {
                this.instructor = instructor;
            }

            public async Task<Unit> Handle(CrearInstructor request, CancellationToken cancellationToken)
            {
                InstructorModel datos = new InstructorModel {
                        Nombre = request.Nombre,
                        Apellidos = request.Apellidos,
                        Grado = request.Grado
                };
                var resultado = await instructor.Crear(datos);
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Instructor");
            }
        }
    }
}