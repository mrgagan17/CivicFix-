using System;
using Microsoft.AspNetCore.Http;

namespace CivicFix.Models
{
    public class Complaint
    {
        public int ComplaintID { get; set; }

        public int UserID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

        public DateTime DateCreated { get; set; }

        public string ImagePath { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}