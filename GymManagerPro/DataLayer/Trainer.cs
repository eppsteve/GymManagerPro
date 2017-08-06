using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GymManagerPro.DataLayer
{
    public class Trainer
    {
        public int TrainerID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public string Notes { get; set; }
        //public byte[] Image { get; set; }

        public void LoadTrainer(SqlDataReader reader)
        {
            TrainerID = reader.GetInt32(0);
            FName = reader["FirstName"].ToString();
            LName = reader["LastName"].ToString();
            Sex = reader["Sex"].ToString();
            if (!reader.IsDBNull(3))
            {
                DateOfBirth = reader.GetDateTime(3);
            }
            Street = reader["Street"].ToString();
            Suburb = reader["Suburb"].ToString();
            City = reader["City"].ToString();
            HomePhone = reader["HomePhone"].ToString();
            CellPhone = reader["CellPhone"].ToString();
            Email = reader["email"].ToString();
            if (!reader.IsDBNull(11))
            {
                Salary = reader.GetDecimal(11);
            }
            Notes = reader["Notes"].ToString();
        }
    }

    public class Trainers
    {
        /// <summary>
        /// retrieves id and lastname from all trainers
        /// </summary>
        /// <returns></returns>
        //public static DataTable GetAllTrainers()
        //{
        //    using (SqlConnection con = DB.GetSqlConnection())
        //    {
        //        string query = "SELECT Id, (FirstName + LastName) AS Name FROM Trainers";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        SqlCeDataAdapter sda = new SqlCeDataAdapter();
        //        sda.SelectCommand = cmd;
        //        DataTable dataset = new DataTable();
        //        sda.Fill(dataset);
        //        sda.Update(dataset);
        //        return dataset;
        //    }
        //}

        public static Dictionary<int, string> GetAllTrainers()
        {
            Dictionary<int, string> trainers = new Dictionary<int,string>();
            using (SqlConnection con = DB.GetSqlConnection())
            {
                string query = "SELECT Id, (FirstName + ' ' + LastName) AS Name FROM Trainers";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trainers.Add(reader.GetInt32(0), reader.GetString(1));
                    }

                    return trainers;
                }
            }
        }

        /// <summary>
        /// retrieves the specified trainer
        /// </summary>
        /// <param name="id">trainer's id</param>
        /// <returns></returns>
        public static Trainer GetTrainer(int id)
        {
            Trainer trainer = new Trainer();
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "SELECT * FROM Trainers WHERE Id = @memberID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@memberid", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trainer.LoadTrainer(reader);
                    }
                }
                return trainer;
            }
        }

        /// <summary>
        /// retrieves the members that are associated with the specified personal trainer
        /// </summary>
        /// <param name="id">trainer's id</param>
        /// <returns></returns>
        public static DataTable GetAssociatedMembers(int id)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                string query = "SELECT Members.Id, Members.FirstName, Members.LastName, Members.CardNumber, Members.CellPhone FROM Members " +
                               "JOIN Trainers ON Members.PersonalTrainer = Trainers.Id " +
                               "WHERE Trainers.Id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dataset = new DataTable();
                sda.Fill(dataset);
                sda.Update(dataset);
                return dataset;
            }
        }

        /// <summary>
        ///  retrieves the last inserted trainer
        /// </summary>
        /// <returns></returns>
        public static int GetLastInsertedTrainer()
        {
            int id = (int)default(int);
            //string query = "SELECT Id FROM Trainers WHERE ID = IDENT_CURRENT('Trainers')";
            string query = "SELECT MAX(Id) FROM Trainers";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }
            return id;
        }

        /// <summary>
        /// adds a new trainer
        /// </summary>
        /// <param name="trainer"></param>
        public static int NewTrainer(Trainer trainer)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                string query = "INSERT INTO Trainers (FirstName, LAstName, Sex, DOB, Street, Suburb, City, HomePhone, CellPhone, Email, Salary, Notes) " +
                               "VALUES (@fname, @lname, @sex, @dob, @street, @suburb, @city, @homephone, @cellphone, @email, @salary, @notes)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@fname", trainer.FName);
                cmd.Parameters.AddWithValue("@lname", trainer.LName);
                cmd.Parameters.AddWithValue("@sex", trainer.Sex);
                cmd.Parameters.AddWithValue("@dob", trainer.DateOfBirth);
                cmd.Parameters.AddWithValue("@street", trainer.Street);
                cmd.Parameters.AddWithValue("@suburb", trainer.Suburb);
                cmd.Parameters.AddWithValue("@city", trainer.City);
                //cmd.Parameters.AddWithValue("@pcode", trainer.PostalCode);
                cmd.Parameters.AddWithValue("@homephone", trainer.HomePhone);
                cmd.Parameters.AddWithValue("@cellphone", trainer.CellPhone);
                cmd.Parameters.AddWithValue("@email", trainer.Email);
                cmd.Parameters.AddWithValue("@salary", trainer.Salary);
                cmd.Parameters.AddWithValue("@notes", trainer.Notes);

                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }

        /// <summary>
        /// saves trainer's details
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns></returns>
        public static int UpdateTrainer(Trainer trainer)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "UPDATE Trainers " +
                           "SET LastName = @lastname, salary = @salary, " +
                           "FirstName = @firstname, Sex = @Sex, DOB = @dateofbirth, Street = @street, " +
                           "Suburb = @suburb, City = @city, HomePhone = @homephone, " +
                           "CellPhone = @cellphone, email = @email, Notes = @notes " +
                           "WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", trainer.TrainerID);
                    cmd.Parameters.AddWithValue("@salary", trainer.Salary);
                    cmd.Parameters.AddWithValue("@lastname", trainer.LName);
                    cmd.Parameters.AddWithValue("@firstname", trainer.FName);
                    cmd.Parameters.AddWithValue("@sex", trainer.Sex);
                    cmd.Parameters.AddWithValue("@dateofbirth", trainer.DateOfBirth);
                    cmd.Parameters.AddWithValue("@street", trainer.Street);
                    cmd.Parameters.AddWithValue("@suburb", trainer.Suburb);
                    cmd.Parameters.AddWithValue("@city", trainer.City);
                    //cmd.Parameters.AddWithValue("@postalcode", trainer.PostalCode);
                    cmd.Parameters.AddWithValue("@homephone", trainer.HomePhone);
                    cmd.Parameters.AddWithValue("@cellphone", trainer.CellPhone);
                    cmd.Parameters.AddWithValue("@email", trainer.Email);
                    cmd.Parameters.AddWithValue("@notes", trainer.Notes);
                    //cmd.Parameters.AddWithValue("@image", trainer.Image);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }  
            }
        }

        /// <summary>
        /// deletes specified trainer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteTrainer(int id)
        {
            String query = "DELETE FROM Trainers WHERE Id = @id";
            using (SqlConnection con = DB.GetSqlConnection()){
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
                
        }

        /// <summary>
        /// removes the specified member from trainer's member list
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of affected rows</returns>
        public static int RemoveMember(int id)
        {
            int affected_rows = 0;
            using (SqlConnection con = DB.GetSqlConnection())
            {
                string query = "UPDATE Members SET PersonalTrainer = NULL Where Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    affected_rows = cmd.ExecuteNonQuery();
                    return affected_rows;
                }
            }
        }

        /// <summary>
        /// set specified trainer as a personal trainer for specified member
        /// </summary>
        /// <param name="trainerid"></param>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int AssignTrainerToMember(int trainerid, int memberid)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                string query = "UPDATE Members SET PersonalTrainer = @trainerid WHERE Id = @memberid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@trainerid", trainerid);
                cmd.Parameters.AddWithValue("@memberid", memberid);
                int rows_affected = cmd.ExecuteNonQuery();
                return rows_affected;
            }
        }

    }
}
