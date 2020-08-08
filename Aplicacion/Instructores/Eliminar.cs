using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{
    public class Eliminar
    {
        public class BorrarInstructor : IRequest {
            public Guid Id { get; set; }
        }

        public class ManejarBorrarInstructor : IRequestHandler<BorrarInstructor>
        {
            private readonly IInstructor instructor;

            public ManejarBorrarInstructor(IInstructor instructor)
            {
                this.instructor = instructor;
            }

            public async Task<Unit> Handle(BorrarInstructor request, CancellationToken cancellationToken)
            {
                var resultados = await instructor.Eliminar(request.Id);
                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo eliminar el instructor");
            }
        }
    }
}