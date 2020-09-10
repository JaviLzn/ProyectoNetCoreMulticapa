using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFactoryConnection factoryConnection;

        public PaginacionRepositorio(IFactoryConnection factoryConnection)
        {
            this.factoryConnection = factoryConnection;
        }

        public async Task<PaginacionModel> devolverPaginacion(string nombreProcedimientoAlmacenado, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamiento)
        {
            PaginacionModel paginacionModel = new PaginacionModel();
            try
            {
                var cnx = factoryConnection.GetConnection();

                // * Parametros
                DynamicParameters parameteros = new DynamicParameters();
                // ? Parametros de entrada
                // ! Filtros
                foreach (var param in parametrosFiltro)
                {
                    parameteros.Add("@" + param.Key, param.Value);
                }
                parameteros.Add("@NumeroPagina", numeroPagina);
                parameteros.Add("@CantidadElementos", cantidadElementos);
                parameteros.Add("@Ordenamiento", ordenamiento);
                // ? Parametros de Salida
                parameteros.Add("@TotalRegistros", null, DbType.Int32, ParameterDirection.Output);
                parameteros.Add("@CantidadPaginas", null, DbType.Int32, ParameterDirection.Output);
                var resultado = await cnx.QueryAsync(nombreProcedimientoAlmacenado, parameteros, commandType: CommandType.StoredProcedure);

                paginacionModel.ListaRegistros = resultado.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionModel.TotalRegistros = parameteros.Get<int>("TotalRegistros");
                paginacionModel.CantidadPaginas = parameteros.Get<int>("CantidadPaginas");
                return paginacionModel;
            }
            catch (Exception e)
            {
                throw new Exception("No se puedo ejecutar el procedimiento almacenado", e);
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
        }
    }
}