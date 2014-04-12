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

        public void LoadTrainer(SqlCeDataReader reader)
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
        public static DataTable GetAllTrainers()
        {
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                string query = "SELECT Id, Surname FROM Trainers";
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
        /// retrieves the specified trainer
        /// </summary>
        /// <param name="id">trainer's id</param>
        /// <returns></returns>
        public static Trainer GetTrainer(int id)
        {
            Trainer trainer = new Trainer();
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                String query = "SELECT * FROM Trainers WHERE Id = @memberID";

                using (SqlCeCommand cmd = new SqlCeCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@memberid", id);
                    SqlCeDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trainer.LoadTrainer(reader);
                    }
                }
                return trainer;
            }
        }

        /// <summary>
        /// gets personal trainer's name by specified member id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>trainer id</returns>
        public static string GetTrainerNameById(int id)
        {
            string name = null;
            string query = "SELECT (Trainers.Name + ' ' + Trainers.Surname) AS PTrainer FROM Members " +
                            "JOIN Trainers ON Members.PersonalTrainer = Trainers.Id " +
                            "WHERE Members.Id = @id";

            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                SqlCeDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name = reader["PTrainer"].ToString();
                }
                return name;
            }
        }

        /// <summary>
        /// retrieves the members that are associated with the specified personal trainer
        /// </summary>
        /// <param name="id">trainer's id</param>
        /// <returns></returns>
        public static DataTable GetAssociatedMembers(int id)
        {
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                string query = "SELECT Members.Id, Members.FirstName, Members.LastName, Members.CardNumber, Members.CellPhone FROM Members " +
                               "JOIN Trainers ON Members.PersonalTrainer = Trainers.Id " +
                               "WHERE Trainers.Id = @id";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlCeDataAdapter sda = new SqlCeDataAdapter();
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
            string query = "SELECT Id FROM Trainers WHERE ID = IDENT_CURRENT('Trainers')";

            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                SqlCeDataReader reader = cmd.ExecuteReader();

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
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                string query = "INSERT INTO Trainers (Name, Surname, Sex, DateOfBirth, Street, Suburb, City, HomePhone, CellPhone, email, salary, Notes) " +
                               "VALUES (@fname, @lname, @sex, @dob, @street, @suburb, @city, @homephone, @cellphone, @email, @salary, @notes)";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
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
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                String query = "UPDATE Trainers " +
                           "SET Surname = @lastname, salary = @salary, " +
                           "Name = @firstname, Sex = @Sex, DateOfBirth = @dateofbirth, Street = @street, " +
                           "Suburb = @suburb, City = @city, HomePhone = @homephone, " +
                           "CellPhone = @cellphone, email = @email, Notes = @notes " +
                           "WHERE Id = @id";
                using (SqlCeCommand cmd = new SqlCeCommand(query, con))
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
            using (SqlCeConnection con = DB.GetSqlCeConnection()){
                SqlCeCommand cmd = new SqlCeCommand(query, con);
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
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                string query = "UPDATE Members SET PersonalTrainer = NULL Where Id = @id";
                using (SqlCeCommand cmd = new SqlCeCommand(query, con))
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
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                string query = "UPDATE Members SET PersonalTrainer = @trainerid WHERE Id = @memberid";
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@trainerid", trainerid);
                cmd.Parameters.AddWithValue("@memberid", memberid);
                int rows_affected = cmd.ExecuteNonQuery();
                return rows_affected;
            }
        }

    }
}
