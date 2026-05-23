using Microsoft.AspNetCore.Mvc;
using CivicFix.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CivicFix.Controllers
{
    public class AdminController : Controller
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CivicFixDB;Trusted_Connection=True;";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("Admin", "true");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid Admin Login";
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            List<Complaint> complaints = new List<Complaint>();

            SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT * FROM Complaints";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Complaint c = new Complaint();

                c.ComplaintID = (int)reader["ComplaintID"];
                c.Title = reader["Title"].ToString();
                c.Category = reader["Category"].ToString();
                c.Location = reader["Location"].ToString();
                c.Description = reader["Description"].ToString();
                c.Status = reader["Status"].ToString();

                // IMPORTANT: READ IMAGE PATH
                c.ImagePath = reader["ImagePath"] == DBNull.Value
                    ? null
                    : reader["ImagePath"].ToString();

                complaints.Add(c);
            }

            con.Close();

            return View(complaints);
        }

        public IActionResult Resolve(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);

            string query = "UPDATE Complaints SET Status='Resolved' WHERE ComplaintID=@id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Dashboard");
        }

        public IActionResult Exit()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Index", "Home");
        }
    }
}