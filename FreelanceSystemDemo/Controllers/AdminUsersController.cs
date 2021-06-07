using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.Models;


namespace FreelanceSystemDemo.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminUsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        // GET: AdminUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: AdminUsers/Details/5
        public ActionResult Details(string id)
        { 
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: AdminUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUsers/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserName,Email,PasswordHash,UserType")] ApplicationUser user)
        {

            ViewBag.UserTypee = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admins")).ToList(), "Name", "Name");

            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser { UserName = user.UserName, UserFirstName = "NULL", UserLastName = "NULL", PhoneNumber = "NULL", UserImage = "NULL", UserType = user.UserType, Email = user.Email };
                var result = await UserManager.CreateAsync(newUser, user.PasswordHash);

                if (result.Succeeded)
                {                    
                    await UserManager.AddToRoleAsync(newUser.Id, user.UserType);
                    return RedirectToAction("Index", "Home");
                }
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: AdminUsers/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: AdminUsers/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,UserName")] ApplicationUser user)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminUsers/Delete/5
        public ActionResult Delete(string id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: AdminUsers/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplicationUser user)
        {
            var userToDelete = db.Users.Find(user.Id);
            db.Users.Remove(userToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }






        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
    }
}
