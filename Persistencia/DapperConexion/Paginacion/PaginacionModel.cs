using System.Collections.Generic;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionModel
    {
        public List<IDictionary<string, object>> ListaRegistros { get; set; }
        public int TotalRegistros { get; set; }
        public int CantidadPaginas { get; set; }
    }
}