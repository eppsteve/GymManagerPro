using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GymManagerPro.DataLayer
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }


        /// <summary>
        /// retrieves the specified plan
        /// </summary>
        /// <param name="id"></param>
        public static Plan GetPlan(int id)
        {
            Plan plan = new Plan();

            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "SELECT * FROM Plans WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    plan.Id = reader.GetInt32(0);
                    plan.Name = reader.GetString(1);
                    plan.Duration = reader.GetInt32(2);
                    plan.Price = reader.GetDecimal(3);
                    if (!reader.IsDBNull(4))
                        plan.Notes = reader.GetString(4);
                }
            }
            return plan;
        }

        /// <summary>
        /// retrieves all programmes from the db
        /// </summary>
        /// <returns>dictionary with all plans</returns>
        public static Dictionary<int, String> GetAllPlans()
        {
            Dictionary<int, string> plans = new Dictionary<int, string>();

            string query = "SELECT Id, Name FROM Plans";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int plan_id = reader.GetInt32(0);
                    string plan_name = reader.GetString(1);

                    plans.Add(plan_id, plan_name);
                }
            }
            return plans;
        }

        /// <summary>
        /// updates the details of specified programme/plan 
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdatePlan(Plan plan)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "UPDATE Plans SET Name = @name, Price = @price, Duration = @duration, Notes = @notes WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", plan.Id);
                cmd.Parameters.AddWithValue("@name", plan.Name);
                cmd.Parameters.AddWithValue("@duration", plan.Duration);
                cmd.Parameters.AddWithValue("@price", plan.Price);
                cmd.Parameters.AddWithValue("@notes", plan.Notes);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }

        public static int DeletePlan(int id)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "DELETE FROM Plans Where Id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }

        public static int CreateNewPlan(Plan plan)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "INSERT INTO Plans (Name, Price, Duration, Notes) VALUES (@name, @price, @duration, @notes)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", plan.Name);
                cmd.Parameters.AddWithValue("@price", plan.Price);
                cmd.Parameters.AddWithValue("@duration", plan.Duration);
                cmd.Parameters.AddWithValue("@notes", plan.Notes);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }

        // retrieves plan duration (in months) for the specified plan
        public static int GetPlanDuration(int plan_id)
        {
            int duration = 0;

            string query = "SELECT Duration FROM Plans WHERE Id = @plan";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@plan", plan_id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    duration = reader.GetInt32(0);
                }
            }
            return duration;
        }

        /// <summary>
        /// retrieves price for the specified plan
        /// </summary>
        /// <param name="plan_id"></param>
        /// <returns>plan's price</returns>
        public static decimal GetPlanPrice(int plan_id)
        {
            decimal price = 0;

            string query = "SELECT Price FROM Plans WHERE Id = @id";

            SqlDataReader reader;

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", plan_id);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    price = reader.GetDecimal(0);
                }
                return price;
            }
        }
    }
}
