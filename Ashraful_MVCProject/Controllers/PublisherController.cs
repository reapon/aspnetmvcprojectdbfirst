using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Ashraful_MVCProject.Models;
using Ashraful_MVCProject.Models.ViewModels;
using AutoMapper;     
using PagedList;
using PagedList.Mvc;

namespace Ashraful_MVCProject.Controllers
{
    public class PublisherController : Controller
    {
        private Ashraful_MVCProjectEntities db = new Ashraful_MVCProjectEntities();
        // GET: Publisher
        [Authorize(Roles = "Admin")]

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var publisher = from t in db.Publishers
                           select t;
            if (!string.IsNullOrEmpty(searchString))
            {
                publisher = publisher.Where(t => t.PublisherName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    publisher = publisher.OrderByDescending(t => t.PublisherName);
                    break;
                default:
                    publisher = publisher.OrderBy(t => t.PublisherName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(publisher.ToPagedList(pageNumber, pageSize));
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PublisherVM publisherVM)
        {
            if (ModelState.IsValid)
            {
                var publisher= Mapper.Map<Publisher>(publisherVM);
                db.Publishers.Add(publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisherVM);
        }


        public ActionResult Edit(int? id)
        {
            var query = db.Publishers.Single(t => t.PublisherID == id);
            var publisher = Mapper.Map<Publisher, PublisherVM>(query);
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PublisherVM publisherVM )
        {
            if (ModelState.IsValid)
            {
                var publisher = Mapper.Map<Publisher>(publisherVM);
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisherVM);
        }



        public ActionResult Delete(int? id)
        {
            var query = db.Publishers.Single(p => p.PublisherID == id);
            var publisher = Mapper.Map<Publisher, PublisherVM>(query);
            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, PublisherVM publisherVM)
        {
            var query = db.Publishers.Single(p => p.PublisherID == id);
            var publisher = Mapper.Map<Publisher, PublisherVM>(query);
            db.Publishers.Remove(query);  // 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = db.Publishers.Single(p => p.PublisherID == id);
            var publisher = Mapper.Map<Publisher, PublisherVM>(query);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

    }
}