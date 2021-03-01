using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ashraful_MVCProject.Models;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace Ashraful_MVCProject.Controllers
{

    public class CustomerController : Controller
    {
        private Ashraful_MVCProjectEntities db = new Ashraful_MVCProjectEntities();


        public ActionResult Index()
        {
            ViewBag.CountryList = new SelectList(GetCountryList(), "CountryID", "CountryName");

            return View();
        }

        public List<Country> GetCountryList()
        {

            Ashraful_MVCProjectEntities db = new Ashraful_MVCProjectEntities();

            List<Country> countries = db.Countries.ToList();

            return countries;
        }

        public ActionResult GetStateList(int CountryId)
        {
            Ashraful_MVCProjectEntities db = new Ashraful_MVCProjectEntities();

            List<State> stateList = db.States.Where(x => x.CountryID == CountryId).ToList();

            ViewBag.StateOptions = new SelectList(stateList, "StateID", "StateName");

            return PartialView("StateOptionPartial");
        }

        public ActionResult GetCityList(int stateId)
        {
            Ashraful_MVCProjectEntities db = new Ashraful_MVCProjectEntities();

            List<City> cityList = db.Cities.Where(x => x.StateID == stateId).ToList();

            ViewBag.CityOptions = new SelectList(cityList, "CityID", "CityName");

            return PartialView("CityOptionPartial");

        }

        public ActionResult Create()
        {
            ViewBag.CountryList = new SelectList(GetCountryList(), "CountryID", "CountryName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerRegistraion customerRegistraion)
        {
            ViewBag.CountryList = new SelectList(GetCountryList(), "CountryID", "CountryName");

            if (ModelState.IsValid)
            {
                db.CustomerRegistraions.Add(customerRegistraion);
                db.SaveChanges();

                //Send to Customer

                MailMessage m = new MailMessage(new MailAddress("reapon.test@gmail.com", "Customer Registration"), new MailAddress(customerRegistraion.Email));

                m.Subject = "Registration";
                m.Body = string.Format("Dear {0}, Thank You For Your Registration. <BR/> Now You Can enjoy all facilities of our online Library!!", customerRegistraion.CustomerName);

                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Credentials = new NetworkCredential("reapon.test@gmail.com", "01828074472");
                smtp.EnableSsl = true;
                smtp.Send(m);

                //Send To Authority

                MailMessage mail = new MailMessage(new MailAddress("reapon.test@gmail.com", "Customer Registration Info"), new MailAddress("reapon.test@gmail.com"));

                mail.Subject = "New Customer Registration";
                mail.Body = string.Format("{0} Has Been Registered. <BR/> Some of his/her Information are given below: <BR/> Email : {1} <BR/> Phone : {2} <BR/>", customerRegistraion.CustomerName, customerRegistraion.Email, customerRegistraion.Phone);
                mail.IsBodyHtml = true;
                smtp.Send(mail);

                return RedirectToAction("Success");
            }
            return View();
        }



        public ActionResult Success()
        {
            return View();
        }

    }
}