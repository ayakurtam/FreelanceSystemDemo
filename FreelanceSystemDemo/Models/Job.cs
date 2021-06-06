using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace FreelanceSystemDemo.Models
{
    public class Job
    {
        public int Id { get; set; }

        [DisplayName("Job Name")]
        public string JobTitle { get; set; }

        [DisplayName("Job Description")]
        public string JobContent { get; set; }

        [DisplayName("Job Image")]
        public string JobImage { get; set; }

        [DisplayName("Job Type")]
        public string JobType { get; set; }

        [DisplayName("Job Budget")]
        public int JobBudget { get; set; }

        [DisplayName("Number Of Proposal")]
        public int NumberOfProposal { get; set; }

        [DisplayName("Post Status")]
        public bool PostStatus { get; set; }


        // one to many Relation
        [DisplayName("Job Type")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}