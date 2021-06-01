using FreelanceSystemDemo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = db.Categories.ToList();
            return View(list); // return categories as list
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);

            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId; // Session is temporary to store data to pass JobId
            return View(job);
        }

        // show us the info (as GET)
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost] // send info tpo the server
        public ActionResult Apply(string Message)  // Determine the info that the user want
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];  // casting to int
            // the other values are assigned automatically

            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();

            if (check.Count < 1) { 
                var job = new ApplyForJob();

                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();

                // Dynamic property
                ViewBag.Result = "Added Successfully";
            }
            else
            {
                ViewBag.Result = "You've already applied to this Job";
            }
            return View();
        }

        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}