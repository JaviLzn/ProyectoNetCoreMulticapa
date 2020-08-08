using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Paginacion
{
    public interface IPaginacion
    {
         Task<PaginacionModel> devolverPaginacion(string nombreProcedimientoAlmacenado,
                                                  int numeroPagina,
                                                  int cantidadElementos,
                                                  IDictionary<string, object> parametrosFiltro,
                                                  string ordenamiento);
    }
}