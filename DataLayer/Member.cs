using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Member
    {
        public int MemberID { get; set; }
        public int CardNumber { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street {get; set;}
        public string Suburb { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
        public int PersonalTrainer { get; set; }
        public string Notes { get; set; }
        public byte[] Image { get; set; }

        public void LoadMember(SqlDataReader reader)
        {
            //MemberID = Int32.Parse(reader["Id"].ToString());
            MemberID = reader.GetInt32(0);
            CardNumber = reader.GetInt32(1);
            LName = reader["LastName"].ToString();
            FName = reader["FirstName"].ToString();
            Sex = reader["Sex"].ToString();
            DateOfBirth = reader.GetDateTime(5);
            Street = reader["Street"].ToString();
            Suburb = reader["Suburb"].ToString();
            City = reader["City"].ToString();
            PostalCode = reader.GetInt32(9);
            HomePhone = reader["HomePhone"].ToString();
            CellPhone = reader["CellPhone"].ToString();
            Email = reader["email"].ToString();
            Occupation = reader["Occupation"].ToString();
            if (!reader.IsDBNull(14))
            {
                PersonalTrainer = reader.GetInt32(14);
            }
            Notes = reader["Notes"].ToString();
            if (!reader.IsDBNull(16))
            {
                Image = (byte[])reader["Image"];
            }
        }

        //// checks the db columns for null string values
        //private string SafeGetString(SqlDataReader reader, int colIndex)
        //{
        //    if (!reader.IsDBNull(colIndex))
        //        return reader.GetString(colIndex);
        //    else
        //        return string.Empty;
        //}

    }

    public class Members
    {

        /// <summary>
        /// retrieves all members
        /// </summary>
        /// <returns>datatable with all member info</returns>
        public static DataTable GetAllMembers()
        {
            DataTable dataset;
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String sql = "SELECT Members.Id, Members.CardNumber, Members.LastName, Members.FirstName, Members.HomePhone, " +
                             "Members.CellPhone, Members.email, (Trainers.Name + ' ' + Trainers.Surname) AS PersonalTrainer " +
                             "FROM Members LEFT OUTER JOIN Trainers ON Members.PersonalTrainer = Trainers.Id";
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                dataset = new DataTable();
                sda.Fill(dataset);
                    //BindingSource bSource = new BindingSource();
                    //bSource.DataSource = dataset;
                    //membersDataGridView.DataSource = bSource;
                sda.Update(dataset);
                return dataset;
            }
        }

        /// <summary>
        /// returns a member
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public Member GetMember(int memberID)
        {
            Member member = new Member();

            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "SELECT * FROM Members WHERE Id = @memberID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@memberid", memberID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        member.LoadMember(reader);
                    }

                }
            }
            return member;
        }

        /// <summary>
        /// retrieves membership details for a given member
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public static DataTable GetMembership(int memberID)
        {
            DataTable table = new DataTable();
            SqlDataAdapter da = null;

            string query = "SELECT Memberships.Id, Memberships.Programme, Memberships.StartDate, Memberships.EndDate, Programmes.Price " +
                           "FROM Memberships " +
                           "JOIN Programmes " +
                           "ON Memberships.Programme = Programmes.Name " +
                           "JOIN Members " +
                           "ON Memberships.Member = Members.Id " +
                           "WHERE Members.Id = @memberID";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@memberID", memberID);

                da = new SqlDataAdapter(cmd);
                da.Fill(table);
            }

            return table;
        }

        /// <summary>
        /// updates database with new data
        /// </summary>
        /// <param name="member"></param>
        public static int UpdateMember(Member member)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "UPDATE Members " +
                           "SET CardNumber= @cardnumber, LastName= @lastname, " +
                           "FirstName = @firstname, Sex = @Sex, DateOfBirth = @dateofbirth, Street = @street, " +
                           "Suburb = @suburb, City = @city, PostalCode = @postalcode, HomePhone = @homephone, " +
                           "CellPhone = @cellphone, email = @email, Occupation = @occupation, Notes = @notes, " +
                           "Image = @image "  +
                           "WHERE Id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", member.MemberID);
                    cmd.Parameters.AddWithValue("@cardnumber", member.CardNumber);
                    cmd.Parameters.AddWithValue("@lastname", member.LName);
                    cmd.Parameters.AddWithValue("@firstname", member.FName);
                    cmd.Parameters.AddWithValue("@sex", member.Sex);
                    cmd.Parameters.AddWithValue("@dateofbirth", member.DateOfBirth);
                    cmd.Parameters.AddWithValue("@street", member.Street);
                    cmd.Parameters.AddWithValue("@suburb", member.Suburb);
                    cmd.Parameters.AddWithValue("@city", member.City);
                    cmd.Parameters.AddWithValue("@postalcode", member.PostalCode);
                    cmd.Parameters.AddWithValue("@homephone", member.HomePhone);
                    cmd.Parameters.AddWithValue("@cellphone", member.CellPhone);
                    cmd.Parameters.AddWithValue("@email", member.Email);
                    cmd.Parameters.AddWithValue("@occupation", member.Occupation);
                    cmd.Parameters.AddWithValue("@notes", member.Notes);
                    cmd.Parameters.AddWithValue("@image", member.Image);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;  
                }  
            }
        }

        /// <summary>
        /// adds a new member
        /// </summary>
        /// <param name="member"></param>
        /// <returns>number of affected rows</returns>
        public static int AddNewMember(Member member)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "INSERT INTO Members (CardNumber, LastName, FirstName, Sex, DateOfBirth, Street, Suburb, City, PostalCode, HomePhone, CellPhone, email, Occupation, Notes, Image) " +
                               "VALUES(@cardnumber, @lastname, @firstname, @sex, @dateofbirth, @street, @suburb, @city, @postalcode, @homephone, @cellphone, @email, @occupation, @notes, @image)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                   // cmd.Parameters.AddWithValue("@id", member.MemberID);
                    cmd.Parameters.AddWithValue("@cardnumber", member.CardNumber);
                    cmd.Parameters.AddWithValue("@lastname", member.LName);
                    cmd.Parameters.AddWithValue("@firstname", member.FName);
                    cmd.Parameters.AddWithValue("@sex", member.Sex);
                    cmd.Parameters.AddWithValue("@dateofbirth", member.DateOfBirth);
                    cmd.Parameters.AddWithValue("@street", member.Street);
                    cmd.Parameters.AddWithValue("@suburb", member.Suburb);
                    cmd.Parameters.AddWithValue("@city", member.City);
                    cmd.Parameters.AddWithValue("@postalcode", member.PostalCode);
                    cmd.Parameters.AddWithValue("@homephone", member.HomePhone);
                    cmd.Parameters.AddWithValue("@cellphone", member.CellPhone);
                    cmd.Parameters.AddWithValue("@email", member.Email);
                    cmd.Parameters.AddWithValue("@occupation", member.Occupation);
                    cmd.Parameters.AddWithValue("@notes", member.Notes);
                    cmd.Parameters.AddWithValue("@image", member.Image);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;

                }
            }
        }

        /// <summary>
        /// deletes the specified member
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of affected rows</returns>
        public static int DeleteMember(int id)
        {
            string query = "DELETE FROM Members WHERE Id = @id";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        /// <summary>
        /// checks if there are more members
        /// </summary>
        /// <param name="currentMemberID"></param>
        /// <returns></returns>
        public static bool MemberHasNext(int currentMemberID)
        {
            bool hasnext = false;
            string query = "SELECT MIN(Id) FROM Members WHERE Id>@currentId";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@currentId", currentMemberID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        hasnext = true;
                    } else
                    {
                        hasnext = false;
                    }
                }
                return hasnext;
            }
        }

        public static bool MemberHasPrevious(int currentMemberID)
        {
            bool hasprev = false;
            string query = "SELECT MAX(Id) FROM Members WHERE Id<@currentId";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@currentId", currentMemberID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        hasprev = true;
                    }
                    else
                    {
                        hasprev = false;
                    }
                }
                return hasprev;
            }
        }

        /// <summary>
        /// returns the id of the next member
        /// </summary>
        /// <param name="currentMemberID"></param>
        /// <returns></returns>
        public static int GetNextMember(int currentMemberID)
        {
            int nextId = 0;
            string query = "SELECT MIN(Id) FROM Members WHERE Id>@currentId";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@currentId", currentMemberID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        nextId = reader.GetInt32(0);
                    }
                }

                return nextId;
            }
        }

        /// <summary>
        /// returns the id of the previous member
        /// </summary>
        /// <param name="currentMemberID"></param>
        /// <returns></returns>
        public static int GetPrevMember(int currentMemberID)
        {
            int prevId = 0;
            string query = "SELECT MAX(Id) FROM Members WHERE Id<@currentId";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@currentId", currentMemberID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        prevId = reader.GetInt32(0);
                    }
                }

                return prevId;
            }
        }

        /// <summary>
        /// checks-in a member and returns the check-in time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int MemberCheckin(int id)
        {
            //DateTime datetime = DateTime.Now;
            //String dt = datetime.ToString();
            int rowsAffected = 0;
            String query = "INSERT INTO Checkin VALUES(@id, @time)";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        /// <summary>
        /// retrieves all programmes from the db
        /// </summary>
        /// <returns>list with all programmes</returns>
        public static List<String> GetAllProgrammes()
        {
            //System.Windows.Forms.ComboBox cb = new System.Windows.Forms.ComboBox();
            List<string> programmes = new List<string>();

            string query = "SELECT Name FROM Programmes";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string programme = reader.GetString(0);
                    //cb.Items.Add(programme);
                    programmes.Add(programme);
                }
            }
            return programmes;
        }

        /// <summary>
        /// adds a new membership for the specified member
        /// </summary>
        /// <param name="id"></param>
        /// <param name="programme"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>number of affected rows</returns>
        public static int AddNewMembership(int id, string programme, DateTime startDate, DateTime endDate)
        {
            //DateTime startDate = datePickerStart.Value.Date;
            //DateTime endDate = datePickerEnd.Value.Date;

            string query = "INSERT INTO Memberships (Member, Programme, StartDate, EndDate) VALUES (@id, @programme, @startDate, @endDate)";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@programme", programme);
                cmd.Parameters.AddWithValue("@StartDate", startDate.Date);
                cmd.Parameters.AddWithValue("@EndDate", endDate.Date);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        /// <summary>
        /// deletes the specified membership
        /// </summary>
        /// <param name="id"></param>
        /// <returns>number of affected rows</returns>
        public static int DeleteMembership(int id)
        {
            string query = "DELETE FROM Memberships WHERE Id = @id";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        /// <summary>
        /// updates specified membership of the specified member
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="membershipID"></param>
        /// <param name="programme"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>number of affected rows</returns>
        public static int UpdateMembership(int memberID, int membershipID, string programme, DateTime startDate, DateTime endDate)
        {
            string query = "UPDATE Memberships SET Programme = @programme, StartDate = @startdate, EndDate = @enddate WHERE Member = @memberid AND Id = @membershipid";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@programme", programme);
                cmd.Parameters.AddWithValue("@startdate", startDate.Date);
                cmd.Parameters.AddWithValue("@enddate", endDate.Date);
                cmd.Parameters.AddWithValue("@memberid", memberID);
                cmd.Parameters.AddWithValue("@membershipid", membershipID);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        /// <summary>
        /// retrieves programme duration for the specified programme
        /// </summary>
        /// <param name="programme"></param>
        /// <returns>programme duration in months</returns>
        public static int GetProgrammeDuration(string programme)
        {
            int duration = 0;

            string query = "SELECT Duration FROM Programmes WHERE Name = @programmeName";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@programmeName", programme);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    duration = reader.GetInt32(0);
                }
            }
            return duration;
        }

        /// <summary>
        /// retrieves price for the specified programme
        /// </summary>
        /// <param name="programme"></param>
        /// <returns>program's price</returns>
        public static decimal GetProgrammePrice(string programme)
        {
            decimal price = 0;

            string query = "SELECT Price FROM Programmes WHERE Name = @programmeName";
            
            SqlDataReader reader;

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@programmeName", programme);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    price = reader.GetDecimal(0);
                }
                return price;
            } 
        }

        /// <summary>
        /// retrieves the last inserted member
        /// </summary>
        /// <returns>id of the last inserted member</returns>
        public static int GetLastInsertedMember()
        {
            int id = (int)default(int);
            string query = "SELECT Id FROM Members WHERE  ID = IDENT_CURRENT('Members')";

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
        /// sets up auto complete searchbox
        /// </summary>
        /// <returns>list with all possible names</returns>
        public static List<string> AutoCompleteSearch()
        {
            List<string> names = new List<string>();

            string query = "SELECT LastName FROM Members";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sName = reader.GetString(0).Trim();
                    names.Add(sName);
                }
                return names;
            }
        }

        /// <summary>
        /// finds the id for the corresponding lastname
        /// </summary>
        /// <param name="name"></param>
        /// <returns>id of the member we searched for</returns>
        public static int QuickSearch(string name)
        {
            string query = "SELECT Id FROM Members WHERE LastName = @lastname";
            int searchId = 0;

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@lastname", name);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    searchId = reader.GetInt32(0);
                }
                return searchId;
            }
        }

        /// <summary>
        /// gets all the check-ins for all members
        /// </summary>
        /// <returns></returns>
        public static StringBuilder GetAttedance()
        {
            StringBuilder data = new StringBuilder();
            string query = "SELECT Members.FirstName, Members.LastName, Checkin.Datetime FROM Checkin " +
                           "JOIN Members ON Checkin.Member = Members.Id";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Append(reader["Datetime"].ToString() +"\t");
                    data.Append(reader["FirstName"].ToString() + " " + reader["LastName"].ToString());
                    data.Append(Environment.NewLine);
                }
            }
            return data;
        }

        /// <summary>
        /// gets the member's name by specified id
        /// </summary>
        /// <param name="name"></param>
        /// <returns>member name</returns>
        public static int GetMemberIdByName(string name)
        {
            int id = 0;
            string query = "SELECT Id FROM Members WHERE LastName = @lname";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@lname", name);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }
            return id;
        }

        /// <summary>
        /// checkin specified member
        /// </summary>
        /// <param name="id">member's id</param>
        /// <returns>number of affected rows</returns>
        public static int CheckInMember(int id)
        {
            string query = "INSERT INTO Checkin (Member, Datetime) VALUES (@memberID, @datetime)";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@memberID", id);
                cmd.Parameters.AddWithValue("@Datetime", DateTime.Now);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }
    }
}
