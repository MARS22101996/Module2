using System.Configuration;
using System.Data.SqlClient;
using CDP.AdoNet.Interfaces;

namespace CDP.AdoNet.UnitOfWorks
{
    public class UnitOfWorkFactory
    {
        public static IUnitOfWorkAdo Create()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString());

            connection.Open();

            return new AdoNetUnitOfWork(connection, true);
        }
    }
}
