using System.Configuration;
using System.Data.SqlClient;

namespace CDP.AdoNet.UnitOfWorks
{
    public class UnitOfWorkFactory
    {
        public static UnitOfWorkConnected Create()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString());

            connection.Open();

            return new UnitOfWorkConnected(connection, true);
        }
    }
}
