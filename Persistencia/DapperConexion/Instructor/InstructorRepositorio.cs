using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepositorio : IInstructor
    {
        private readonly IFactoryConnection factoryConnection;

        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            this.factoryConnection = factoryConnection;
        }
        public Task<int> Actualizar(InstructorModel datos)
        {
            throw new NotImplementedException();
        }

        public Task<int> Crear(InstructorModel datos)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(InstructorModel datos)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> ListaInstructor = null;
            var storedProcedura = "usp_Obtener_Instructores";
            try
            {
                var connection = factoryConnection.GetConnection();
                ListaInstructor = await connection.QueryAsync<InstructorModel>(storedProcedura, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de SPs", e);
            }
            finally
            {
                factoryConnection.CloseConnection();
            }

            return ListaInstructor;
        }

        public Task<InstructorModel> ObtenerPorId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}