using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

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

        // one to many Relation
        [DisplayName("Job Type")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}