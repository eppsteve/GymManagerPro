using System;
using System.Data;
using System.Data.SqlClient;

namespace GymManagerPro.DataLayer
{
    class Membership
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int Plan { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public double price { get; set; }

    }

    class Memberships
    {

        // inserts a new membership in the db
        public static int NewMembership(Membership membership)
        {
            string query = "INSERT INTO Memberships (Member, [Plan], StartDate, EndDate) VALUES (@memberid, @planid, @startDate, @endDate)";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@memberid", membership.MemberId);
                cmd.Parameters.AddWithValue("@planid", membership.Plan);
                cmd.Parameters.AddWithValue("@startDate", membership.start);
                cmd.Parameters.AddWithValue("@endDate", membership.end);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }

        }

        // updates mrmbership with new data
        public static int UpdateMembership(Membership membership)
        {
            string query = "UPDATE Memberships SET [Plan] = @planid, StartDate = @startdate, EndDate = @enddate WHERE Id = @membership_id";
            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@planid", membership.Plan);
                cmd.Parameters.AddWithValue("@startdate", membership.start);
                cmd.Parameters.AddWithValue("@enddate", membership.end);
                cmd.Parameters.AddWithValue("@membership_id", membership.Id);
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
        /// retrieves membership details for a given member
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public static DataTable GetMembershipByMemberId(int memberID)
        {
            DataTable table = new DataTable();
            SqlDataAdapter da = null;
            
            string query = "SELECT        Memberships.Id AS [Membership Id], Plans.Name, Memberships.StartDate AS [Start Date], Memberships.EndDate AS [End Date], Plans.Price " +
                           "FROM          Plans INNER JOIN " +
                           "                 Memberships INNER JOIN " +
                           "                  Members ON Memberships.Member = Members.Id ON Plans.Id = Memberships.[Plan] " +
                           "WHERE        (Members.Id = @memberId)";

            using (SqlConnection con = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@memberId", memberID);

                da = new SqlDataAdapter(cmd);
                da.Fill(table);
            }

            return table;
        }

    }
}
