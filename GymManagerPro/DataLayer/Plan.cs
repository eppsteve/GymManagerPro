using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;

namespace DataLayer
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }


        /// <summary>
        /// retrieves the specified plan
        /// </summary>
        /// <param name="id"></param>
        public static Plan GetPlan(int id)
        {
            Plan plan = new Plan();

            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                String query = "SELECT * FROM Plans WHERE Id = @id";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlCeDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    plan.Id = reader.GetInt32(0);
                    plan.Name = reader.GetString(1);
                    plan.Price = reader.GetDecimal(2);
                    plan.Duration = reader.GetInt32(3);
                }
            }
            return plan;
        }

        /// <summary>
        /// retrieves all membereship plans/programmes
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllPlans()
        {
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                string query = "SELECT * FROM Plans";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                SqlCeDataAdapter sda = new SqlCeDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dataset = new DataTable();
                sda.Fill(dataset);
                sda.Update(dataset);
                return dataset;
            }
        }

        /// <summary>
        /// updates the details of specified programme/plan 
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdatePlan(Plan plan)
        {
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                String query = "UPDATE Plans SET Name = @name, Price = @price, Duration = @duration WHERE Id = @id";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@id", plan.Id);
                cmd.Parameters.AddWithValue("@name", plan.Name);
                cmd.Parameters.AddWithValue("@duration", plan.Duration);
                cmd.Parameters.AddWithValue("@price", plan.Price);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }

        public static int DeletePlan(int id)
        {
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                String query = "DELETE FROM Plans Where Id = @id";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }

        public static int CreateNewPlan(Plan plan)
        {
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                String query = "INSERT INTO Plans (Name, Price, Duration) VALUES (@name, @price, @duration)";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@name", plan.Name);
                cmd.Parameters.AddWithValue("@price", plan.Price);
                cmd.Parameters.AddWithValue("@duration", plan.Duration);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }


    }
}
