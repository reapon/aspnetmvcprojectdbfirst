using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ashraful_MVCProject.Models;

namespace Ashraful_MVCProject.Controllers
{
    public class TestAuthorController : Controller
    {
        private Ashraful_MVCProjectEntities _context;
        public TestAuthorController()
        {
            _context = new Ashraful_MVCProjectEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(_context.Authors.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(_context.Authors.FirstOrDefault(x => x.AuthorID == ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Author author)
        {
            var data = _context.Authors.FirstOrDefault(x => x.AuthorID == author.AuthorID);
            if (data != null)
            {
                data.AuthorName = author.AuthorName;
                data.Email = author.Email;
                data.Phone = author.Phone;
                data.BirthDate = author.BirthDate;
                data.Address = author.Address;

                _context.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            var data = _context.Authors.FirstOrDefault(x => x.AuthorID == ID);
            _context.Authors.Remove(data);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}