using System.Configuration;
using System.Data.SqlClient;

namespace MovieBookingManagement.Models
{
    public class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["MovieBookingDBConnection"].ConnectionString;

            return new SqlConnection(connectionString);
        }
    }
}