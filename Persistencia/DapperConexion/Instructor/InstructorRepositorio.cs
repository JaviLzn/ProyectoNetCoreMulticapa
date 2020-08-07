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
        public async Task<int> Actualizar(InstructorModel datos)
        {
            var UserStoredProcedure = "usp_Instructor_Editar";
            try
            {
                var connection = factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(UserStoredProcedure, datos, commandType: CommandType.StoredProcedure);
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception("Error en la actualizacion de instructor desde USp", e);
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
        }

        public async Task<int> Crear(InstructorModel datos)
        {
            var UserStoredProcedure = "usp_Instructor_Nuevo";
            try
            {
                datos.InstructorId = Guid.NewGuid();
                var connection = factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(UserStoredProcedure,
                                                              datos,
                                                              commandType: CommandType.StoredProcedure);
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception("Error en la creación de instructor desde USp", e);
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
        }

        public Task<int> Eliminar(InstructorModel datos)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> ListaInstructor = null;
            var storedProcedura = "usp_Instructor_Obtener_Lista";
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