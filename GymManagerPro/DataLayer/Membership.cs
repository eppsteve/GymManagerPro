using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace DataLayer
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

            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@memberid", membership.Id);
                cmd.Parameters.AddWithValue("@planid", membership.Plan);
                cmd.Parameters.AddWithValue("@StartDate", membership.start);
                cmd.Parameters.AddWithValue("@EndDate", membership.end);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }

        }


        public static int UpdateMembership(Membership membership)
        {
            string query = "UPDATE Memberships SET Plan = @planid, StartDate = @startdate, EndDate = @enddate WHERE Member = @memberid AND Id = @membershipid";
            using (SqlCeConnection con = DB.GetSqlCeConnection())
            {
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.Parameters.AddWithValue("@planid", membership.Plan);
                cmd.Parameters.AddWithValue("@startdate", membership.start);
                cmd.Parameters.AddWithValue("@enddate", membership.end);
                cmd.Parameters.AddWithValue("@memberid", membership.MemberId);
                cmd.Parameters.AddWithValue("@membershipid", membership.Id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

    }
}
