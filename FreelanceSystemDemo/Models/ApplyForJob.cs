using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace FreelanceSystemDemo.Models
{
    // This table is the result of Many-to-Mandy relationship between (users and Jobs) tables
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; } // The message that the user want to send (info about the job)
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; } // From details in job view
        public string UserId { get; set; } // from login

        public virtual Job job { get; set; } // For job table
        public virtual ApplicationUser user { get; set; } // For user table
    }
}