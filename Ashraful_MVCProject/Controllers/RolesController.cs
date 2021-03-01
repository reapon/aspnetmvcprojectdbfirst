using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ashraful_MVCProject.Models;
using System.Data.Entity;

namespace Ashraful_MVCProject.Controllers
{
    public class RolesController : Controller
    {
        [Authorize(Roles ="Admin")]
        public ActionResult Start()
        {
            return View();
        }


        // GET: Roles
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            //Populate dropdown list
            var context = new ApplicationDbContext();

            var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;

            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();

            ViewBag.Users = userList;

            ViewBag.Message = "";
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Roles.Add(new IdentityRole() { Name = collection["RoleName"] });
                context.SaveChanges();
                ViewBag.Message = "Role Created Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Delete(string roleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Edit(string roleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]

        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            var context = new ApplicationDbContext();
            if (context == null)
            {
                throw new ArgumentNullException("context", "Context must not be null!!");
            }
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(user.Id, roleName);

            ViewBag.Message = "Role Assigned Successfully";


            // Populate DropdownList
            var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
              new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;

            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
              new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;

            return View("Index");

        }

        // Getting a list of Roles for user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]

        public ActionResult GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var context = new ApplicationDbContext();
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                ViewBag.RolesForThisUser = userManager.GetRoles(user.Id);
                // Populate DropdownList
                var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
                  new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = roleList;

                var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
                  new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
                ViewBag.Users = userList;

                ViewBag.Message = "Role Retrieved Successfully!!!";

            }
            return View("Index");
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            var account = new AccountController();
            var context = new ApplicationDbContext();

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (userManager.IsInRole(user.Id, roleName))
            {
                userManager.RemoveFromRole(user.Id, roleName);
                ViewBag.Message = "Role removed from this user successfully";
            }
            else
            {
                ViewBag.Message= "This user doesn't belong to selected role!!!!";
            }

            // Populate DropdownList
            var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
              new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;

            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
              new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;

            return View("Index");

        }








    }
}