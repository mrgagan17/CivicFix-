using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace CivicFix.Models
{
    public class DatabaseHelper
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CivicFixDB;Trusted_Connection=True;";

        // INSERT COMPLAINT
        public void InsertComplaint(Complaint complaint)
        {
            SqlConnection con = new SqlConnection(connectionString);

            string query = @"INSERT INTO Complaints 
                    (UserID, Title, Description, Category, Location, Status, DateCreated, ImagePath)
                    VALUES
                    (@UserID, @Title, @Description, @Category, @Location, @Status, GETDATE(), @ImagePath)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@UserID", complaint.UserID);
            cmd.Parameters.AddWithValue("@Title", complaint.Title);
            cmd.Parameters.AddWithValue("@Description", complaint.Description);
            cmd.Parameters.AddWithValue("@Category", complaint.Category);
            cmd.Parameters.AddWithValue("@Location", complaint.Location);
            cmd.Parameters.AddWithValue("@Status", complaint.Status);

            if (complaint.ImagePath == null)
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ImagePath", complaint.ImagePath);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // GET ALL COMPLAINTS (ADMIN)
        public List<Complaint> GetComplaints()
        {
            List<Complaint> list = new List<Complaint>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Complaints";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Complaint c = new Complaint();

                    c.ComplaintID = Convert.ToInt32(reader["ComplaintID"]);
                    c.UserID = Convert.ToInt32(reader["UserID"]);
                    c.Title = reader["Title"].ToString();
                    c.Description = reader["Description"].ToString();
                    c.Category = reader["Category"].ToString();
                    c.Location = reader["Location"].ToString();
                    c.Status = reader["Status"].ToString();

                    // IMPORTANT
                    c.ImagePath = reader["ImagePath"] == DBNull.Value
                        ? null
                        : reader["ImagePath"].ToString();

                    list.Add(c);
                }
            }

            return list;
        }

        // GET USER COMPLAINTS
        public List<Complaint> GetComplaintsByUser(int userId)
        {
            List<Complaint> list = new List<Complaint>();

            SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT * FROM Complaints WHERE UserID=@uid";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@uid", userId);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Complaint c = new Complaint();

                c.Title = reader["Title"].ToString();
                c.Category = reader["Category"].ToString();
                c.Location = reader["Location"].ToString();
                c.Status = reader["Status"].ToString();

                list.Add(c);
            }

            con.Close();

            return list;
        }
    }
}