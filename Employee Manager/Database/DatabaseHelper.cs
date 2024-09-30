using System.Data.SqlClient;
using System.Configuration;



namespace Employee_Manager.Database
{
    public static class DatabaseHelper
    {
        public static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["EmployeeDbConnection"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
