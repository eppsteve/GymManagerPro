using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;

namespace DataLayer
{
    public class DB
    {
        public static string connectionString
        {
            get
            {
                return @"Data Source=|DataDirectory|\Database.sdf";
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
