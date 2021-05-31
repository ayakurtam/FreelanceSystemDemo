using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FreelanceSystemDemo.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Job Type")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Job Description")]
        public string CategoryDescription { get; set; }

        // Many to one  Relation
        public ICollection<Job> Jobs { get; set; }
    }
}