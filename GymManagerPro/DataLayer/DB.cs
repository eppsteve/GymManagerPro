using System.Data.SqlClient;

namespace GymManagerPro.DataLayer
{
    public class DB
    {
        public static string connectionString
        {
            get
            {
                //return @"Data Source=|DataDirectory|\..\..\Database.sdf";
                //return ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                return @"Server=localhost;Database=GymManager;User Id=sa;Password=123;";
            }
        }

        /// <summary>
        /// returns an open connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
    }
}
