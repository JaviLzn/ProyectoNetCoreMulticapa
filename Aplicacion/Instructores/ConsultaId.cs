using Aplicacion.ManejadorError;
using MediatR;
using Persistencia.DapperConexion.Instructor;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Instructores
{
    public class ConsultaId
    {
        public class ObtenerInstructorPorId : IRequest<InstructorModel>
        {
            public Guid Id { get; set; }
        }

        public class ManejarObtenerInstructorPorId : IRequestHandler<ObtenerInstructorPorId, InstructorModel>
        {
            private readonly IInstructor instructorRepositorio;

            public ManejarObtenerInstructorPorId(IInstructor instructorRepositorio)
            {
                this.instructorRepositorio = instructorRepositorio;
            }

            public async Task<InstructorModel> Handle(ObtenerInstructorPorId request, CancellationToken cancellationToken)
            {
                var instructor = await instructorRepositorio.ObtenerPorId(request.Id);

                if (instructor == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el instructor" });
                }

                return instructor;
            }
        }
    }
}