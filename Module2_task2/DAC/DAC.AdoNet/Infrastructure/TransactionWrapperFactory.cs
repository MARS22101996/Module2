using System.Configuration;
using System.Data.SqlClient;

namespace CDP.AdoNet.Infrastructure
{
    public class TransactionWrapperFactory
    {
        public static TransactionWrapperConnected Create()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString());

            connection.Open();

            return new TransactionWrapperConnected(connection, true);
        }
    }
}
