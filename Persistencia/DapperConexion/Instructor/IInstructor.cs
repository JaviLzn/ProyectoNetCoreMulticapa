using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();
        Task<InstructorModel> ObtenerPorId(Guid id);
        Task<int> Crear(InstructorModel datos);
        Task<int> Actualizar(InstructorModel datos);
        Task<int> Eliminar(Guid id);
    }
}