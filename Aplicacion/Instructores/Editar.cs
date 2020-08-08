using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{
    public class Editar
    {
        public class EditarInstructor : IRequest
        {
            public Guid InstructorId { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Grado { get; set; }
        }

        public class ValidarEditarInstructor : AbstractValidator<EditarInstructor>
        {
            public ValidarEditarInstructor()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Grado).NotEmpty();
            }
        }

        public class ManejarEditarInstructor : IRequestHandler<EditarInstructor>
        {
            private readonly IInstructor instructor;
            public ManejarEditarInstructor(IInstructor instructor)
            {
                this.instructor = instructor;
            }
            public async Task<Unit> Handle(EditarInstructor request, CancellationToken cancellationToken)
            {
                var datos = new InstructorModel
                {
                    InstructorId = request.InstructorId,
                    Nombre = request.Nombre,
                    Apellidos = request.Apellidos,
                    Grado = request.Grado
                };

                var resultado = await instructor.Actualizar(datos);

                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Instructor");
            }
        }

    }
}