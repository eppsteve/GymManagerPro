using System;
using System.Data;
using System.Data.SqlClient;

namespace GymManagerPro.DataLayer
{
    public class Measurement
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime Datecreated { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Bodyfat { get; set; }
        public decimal Chest { get; set; }
        public decimal LArm { get; set; }
        public decimal RArm { get; set; }
        public decimal Waist { get; set; }
        public decimal Abdomen { get; set; }
        public decimal Hips { get; set; }
        public decimal LThigh { get; set; }
        public decimal RThigh { get; set; }
        public decimal LCalf { get; set; }
        public decimal RCalf { get; set; }

        public static DataTable GetAllMeasurements(int id)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String sql = @"SELECT Id,Datecreated Date,Height,Weight,Bodyfat,Chest,LARM Left_Arm,RArm Right_Arm,
                               Waist, Abdomen, Hips, LThigh Left_Tigh, RThigh Right_Tigh, LCalf Left_Calf,RCalf Right_Calf 
                               FROM Measurements WHERE MemberId = @memberid";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@memberid", id);

                SqlDataAdapter sda = new SqlDataAdapter()
                {
                    SelectCommand = cmd
                };
                var dataset = new DataTable();
                sda.Fill(dataset);
                sda.Update(dataset);
                return dataset;
            }
        }

        public static Measurement GetMeasurement(int id)
        {
            var measurement = new Measurement();

            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "SELECT * FROM Measurements WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    measurement.Id = reader.GetInt32(0);
                    measurement.MemberId = reader.GetInt32(1);
                    if (!reader.IsDBNull(2))
                        measurement.Datecreated = reader.GetDateTime(2);
                    if (!reader.IsDBNull(3))
                        measurement.Height = reader.GetDecimal(3);
                    if (!reader.IsDBNull(4))
                        measurement.Weight = reader.GetDecimal(4);
                    if (!reader.IsDBNull(5))
                        measurement.Bodyfat = reader.GetDecimal(5);
                    if (!reader.IsDBNull(6))
                        measurement.Chest = reader.GetDecimal(6);
                    if (!reader.IsDBNull(7))
                        measurement.LArm = reader.GetDecimal(7);
                    if (!reader.IsDBNull(8))
                        measurement.RArm = reader.GetDecimal(8);
                    if (!reader.IsDBNull(9))
                        measurement.Waist = reader.GetDecimal(9);
                    if (!reader.IsDBNull(10))
                        measurement.Abdomen = reader.GetDecimal(10);
                    if (!reader.IsDBNull(11))
                        measurement.Hips = reader.GetDecimal(11);
                    if (!reader.IsDBNull(12))
                        measurement.LThigh = reader.GetDecimal(12);
                    if (!reader.IsDBNull(13))
                        measurement.RThigh = reader.GetDecimal(13);
                    if (!reader.IsDBNull(14))
                        measurement.LCalf = reader.GetDecimal(14);
                    if (!reader.IsDBNull(15))
                        measurement.RThigh = reader.GetDecimal(15);
                }
            }
            return measurement;
        }

        public int Save()
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String sql = @"INSERT INTO Measurements (MemberId,Datecreated,Height,Weight,Bodyfat,Chest,
                                LArm,RArm,Waist,Abdomen,Hips,LThigh,RThigh,LCalf,RCalf) VALUES 
                                (@memberid, @datecreated, @height, @weight, @bodyfat, @chest, @larm, @rarm, @waist,
                                @abdomen, @hips, @lthigh, @rthigh, @lcalf, @rcalf)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@memberid", this.MemberId);
                    cmd.Parameters.AddWithValue("@datecreated", this.Datecreated);
                    cmd.Parameters.AddWithValue("@height", this.Height);
                    cmd.Parameters.AddWithValue("@weight", this.Weight);
                    cmd.Parameters.AddWithValue("@bodyfat", this.Bodyfat);
                    cmd.Parameters.AddWithValue("@chest", this.Chest);
                    cmd.Parameters.AddWithValue("@larm", this.LArm);
                    cmd.Parameters.AddWithValue("@rarm", this.RArm);
                    cmd.Parameters.AddWithValue("@waist", this.Waist);
                    cmd.Parameters.AddWithValue("@abdomen", this.Abdomen);
                    cmd.Parameters.AddWithValue("@hips", this.Hips);
                    cmd.Parameters.AddWithValue("@lthigh", this.LThigh);
                    cmd.Parameters.AddWithValue("@rthigh", this.RThigh);
                    cmd.Parameters.AddWithValue("@lcalf", this.LCalf);
                    cmd.Parameters.AddWithValue("@rcalf", this.RCalf);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public int Update()
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String sql = @"UPDATE Measurements SET MemberId=@memberid, Datecreated=@datecreated,
                                Height=@height, Weight=@weight, Bodyfat=@bodyfat, Chest=@chest,
                                LArm=@larm, RArm=@rarm, Waist=@waist, Abdomen=@abdomen, Hips=@hips, 
                                LThigh=@lthigh, RThigh=@rthigh, LCalf=@lcalf, RCalf=@rcalf 
                                WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@memberid", this.MemberId);
                    cmd.Parameters.AddWithValue("@datecreated", this.Datecreated);
                    cmd.Parameters.AddWithValue("@height", this.Height);
                    cmd.Parameters.AddWithValue("@weight", this.Weight);
                    cmd.Parameters.AddWithValue("@bodyfat", this.Bodyfat);
                    cmd.Parameters.AddWithValue("@chest", this.Chest);
                    cmd.Parameters.AddWithValue("@larm", this.LArm);
                    cmd.Parameters.AddWithValue("@rarm", this.RArm);
                    cmd.Parameters.AddWithValue("@waist", this.Waist);
                    cmd.Parameters.AddWithValue("@abdomen", this.Abdomen);
                    cmd.Parameters.AddWithValue("@hips", this.Hips);
                    cmd.Parameters.AddWithValue("@lthigh", this.LThigh);
                    cmd.Parameters.AddWithValue("@rthigh", this.RThigh);
                    cmd.Parameters.AddWithValue("@lcalf", this.LCalf);
                    cmd.Parameters.AddWithValue("@rcalf", this.RCalf);
                    cmd.Parameters.AddWithValue("@id", this.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public static int DeleteMeasurement(int id)
        {
            using (SqlConnection con = DB.GetSqlConnection())
            {
                String query = "DELETE FROM Measurements Where Id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
        }
    }
}
