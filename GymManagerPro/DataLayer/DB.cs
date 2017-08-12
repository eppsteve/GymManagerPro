using System.Data.SqlClient;

namespace GymManagerPro.DataLayer
{
    public class DB
    {
        public static string connectionString
        {
            get
            {
                return @"Server=localhost;Database=GymManager;Trusted_Connection=True;";
            }
        }

        public static string masterConnectionString => @"Server=localhost;Database=master;Trusted_Connection=True;";

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public static SqlConnection GetMasterSqlConnection()
        {
            SqlConnection con = new SqlConnection(masterConnectionString);
            con.Open();
            return con;
        }
    }
}
