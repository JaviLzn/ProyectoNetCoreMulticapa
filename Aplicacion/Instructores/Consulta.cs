using MediatR;
using Persistencia.DapperConexion.Instructor;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Instructores
{
    public class Consulta
    {
        public class Peticion : IRequest<List<InstructorModel>> { }

        public class ManejarPeticion : IRequestHandler<Peticion, List<InstructorModel>>
        {
            private readonly IInstructor instructorRepositorio;

            public ManejarPeticion(IInstructor instructorRepositorio)
            {
                this.instructorRepositorio = instructorRepositorio;
            }

            public async Task<List<InstructorModel>> Handle(Peticion request, CancellationToken cancellationToken)
            {
                var resultado = await instructorRepositorio.ObtenerLista();
                return resultado.ToList();
            }
        }
    }
}