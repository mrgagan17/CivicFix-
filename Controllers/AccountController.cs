using Microsoft.AspNetCore.Mvc;
using CivicFix.Models;
using Microsoft.Data.SqlClient;

namespace CivicFix.Controllers
{
    public class AccountController : Controller
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CivicFixDB;Trusted_Connection=True;";

        // LOGIN PAGE
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN POST
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int userId = (int)reader["UserID"];

                HttpContext.Session.SetInt32("UserID", userId);

                con.Close();

                return RedirectToAction("Submit", "Complaint");
            }

            con.Close();

            ViewBag.Message = "Invalid Login";
            return View();
        }

        // REGISTER PAGE
        public IActionResult Register()
        {
            return View();
        }

        // REGISTER POST
        [HttpPost]
        public IActionResult Register(User user)
        {
            SqlConnection con = new SqlConnection(connectionString);

            string query = "INSERT INTO Users (Name, Email, Password, Phone) VALUES (@Name,@Email,@Password,@Phone)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            ViewBag.Message = "Registration Successful";

            return RedirectToAction("Login");
        }
    }
}