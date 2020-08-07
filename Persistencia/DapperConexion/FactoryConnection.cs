using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection dapperConnection;
        private readonly IOptions<DapperConfiguracion> config;

        public FactoryConnection(IOptions<DapperConfiguracion> config)
        {
            this.config = config;
        }
        public void CloseConnection()
        {
            if (dapperConnection != null && dapperConnection.State == ConnectionState.Open)
            {
                dapperConnection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if(dapperConnection == null){
                dapperConnection = new SqlConnection(config.Value.CursosOnline);
            }

            if (dapperConnection.State != ConnectionState.Open)
            {
                dapperConnection.Open();
            }

            return dapperConnection;
        }
    }
}