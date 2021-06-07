using FreelanceSystemDemo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var jobProposal =  db.Jobs.Where(a => a.Id == JobId).FirstOrDefault<Job>();
            // the other values are assigned automatically

            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();

            if (check.Count < 1) { 
                var job = new ApplyForJob();

                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;


                jobProposal.NumberOfProposal++;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();

                db.Entry(jobProposal).State = EntityState.Modified;
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

   


        [Authorize]
        public ActionResult GetProposalJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserId
                       select app;


            return View(Jobs.ToList());
        }

        public ActionResult Approve(int id)
        {

            var job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("GetPublishedJobsByUser");
            
        }



        [Authorize]
        public ActionResult GetAppliedJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        [Authorize]
        public ActionResult GetPublishedJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.Jobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }



        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }


        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("GetPublishedJobsByUser");
            }
            return View(job);
        }


        public ActionResult Delete(int id)
        {

            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            // TODO: Add delete logic here
            var myjob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myjob);
            db.SaveChanges();
            return RedirectToAction("GetPublishedJobsByUser");

        }



        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null && endDate == null)
            {
                var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
                            || a.User.UserName.Contains(searchName)).ToList();
                return View(result);
            }
            else if (startDate != null && endDate != null)
            {
                var result = db.Jobs.Where(a => a.PublishDate >= startDate && a.PublishDate <= endDate
                            && (a.User.UserName.Contains(searchName) || a.JobTitle.Contains(searchName))).ToList();
                return View(result);
            }
            return View();
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