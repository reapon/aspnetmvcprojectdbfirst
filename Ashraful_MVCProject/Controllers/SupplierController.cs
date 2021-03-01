using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ashraful_MVCProject.Models;

namespace Ashraful_MVCProject.Controllers
{
    public class SupplierController : Controller
    {
        Ashraful_MVCProjectEntities db = new Ashraful_MVCProjectEntities();

        // GET: Customer
        public ActionResult Index()
        {
            var data = db.Suppliers.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Supplier tr)
        {
            if (ModelState.IsValid == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(tr.ImageFile.FileName);
                string extention = Path.GetExtension(tr.ImageFile.FileName);
                HttpPostedFileBase postedFile = tr.ImageFile;
                int length = postedFile.ContentLength;
                if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extention;
                        tr.ImagePath = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        tr.ImageFile.SaveAs(fileName);
                        db.Suppliers.Add(tr);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {

                            ModelState.Clear();
                            return RedirectToAction("Index", "Supplier");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Data not inserted')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size Should Less Than 1 MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtentionMessage"] = "<script>alert('Format Not Supported')</script>";
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var TraineeRow = db.Suppliers.Where(model => model.SupplierID == id).FirstOrDefault();
            Session["Image"] = TraineeRow.ImagePath;
            return View(TraineeRow);
        }

        [HttpPost]
        public ActionResult Edit(Supplier tr)
        {
            if (ModelState.IsValid == true)
            {
                if (tr.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(tr.ImageFile.FileName);
                    string extention = Path.GetExtension(tr.ImageFile.FileName);
                    HttpPostedFileBase postedFile = tr.ImageFile;
                    int length = postedFile.ContentLength;
                    if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extention;
                            tr.ImagePath = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            tr.ImageFile.SaveAs(fileName);
                            db.Entry(tr).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                ModelState.Clear();
                                return RedirectToAction("Index", "Supplier");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size Should Less Than 1 MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtentionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }
                }
                else
                {
                    tr.ImagePath = Session["Image"].ToString();
                    db.Entry(tr).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Supplier");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                    }
                }
            }
            return View();
        }


        public ActionResult Delete(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();

            }
            return View(supplier);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}