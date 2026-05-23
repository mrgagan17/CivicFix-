using Microsoft.AspNetCore.Mvc;
using CivicFix.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CivicFix.Controllers
{
    public class ComplaintController : Controller
    {
        DatabaseHelper db = new DatabaseHelper();

        private readonly IWebHostEnvironment _env;

        public ComplaintController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Complaint complaint)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            complaint.UserID = userId ?? 0;

            complaint.Status = "Pending";

            // IMAGE SAVE
            if (complaint.ImageFile != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "images");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(complaint.ImageFile.FileName);

                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    complaint.ImageFile.CopyTo(stream);
                }

                complaint.ImagePath = "/images/" + fileName;
            }

            db.InsertComplaint(complaint);

            ViewBag.Message = "Complaint submitted successfully!";

            return View();
        }

        public IActionResult MyComplaints()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            List<Complaint> complaints = db.GetComplaintsByUser(userId ?? 0);

            return View(complaints);
        }

        public IActionResult List()
        {
            List<Complaint> complaints = db.GetComplaints();

            return View(complaints);
        }
    }
}