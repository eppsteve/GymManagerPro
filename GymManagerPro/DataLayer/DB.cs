using System.Data.SqlServerCe;

namespace DataLayer
{
    public class DB
    {
        public static string connectionString
        {
            get
            {
                return @"Data Source=|DataDirectory|\..\..\Database.sdf";
                //return ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
        }

        /// <summary>
        /// returns an open connection
        /// </summary>
        /// <returns></returns>
        public static SqlCeConnection GetSqlCeConnection()
        {
            SqlCeConnection con = new SqlCeConnection(connectionString);
            con.Open();
            return con;
        }
    }
}
