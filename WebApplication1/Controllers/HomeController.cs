using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Models;
using WebApplication1.Utils;
using static Dropbox.Api.TeamLog.SharedLinkAccessLevel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult index()
        {

            return View();
        }
        public ActionResult indexadmin()
        {
            if (BaseHelper.Admin == null)
                RedirectToAction("Loginadmin", "Admins");
            TempData["success"] = "Welcome ☺ " + BaseHelper.Admin.Admin_Name + " into your dashboard ";
            return View();
        }
        public ActionResult Packages()
        {
            return View();
        }
        public ActionResult IndexOrganizer()
        {
            if (BaseHelper.event_organizers == null)
                RedirectToAction("login", "Event_Organizers");

            decimal totalSalary = 0;
            DateTime? hireDate = BaseHelper.event_organizers.Event_Organizer_HireDate;
            DateTime currentDate = DateTime.Now;
            int months = 0;

            if (hireDate != null)
            {

                TimeSpan duration = (TimeSpan)(currentDate - hireDate);
                months = (currentDate.Year - hireDate.Value.Year) * 12 + currentDate.Month - hireDate.Value.Month;
            }
            for (int i=0; i<=months; i++)
            {
                var salary =  20000;
                totalSalary += salary;
            }

            var withDrawAmount = db.Withdraw_Amount.Where(x => x.Organizer_FID == BaseHelper.event_organizers.EventOrganizer_ID && x.IsTranffered == true).Sum(x => (decimal?)x.Withdraw_Amount1) ?? 0;
            if (withDrawAmount >= totalSalary)
            {
                totalSalary = 0;
            }
            else
            {
                totalSalary = totalSalary - withDrawAmount;
            }
            BaseHelper.totalsalary = totalSalary;
            TempData["success"] = "Welcome ☺ " + BaseHelper.event_organizers.EventOrganizer_Name + " into your dashboard ";
            return View();
        }

        
        [HttpPost]
        public ActionResult SearchHall(string slot, string location, decimal? pricerange)
        {
            var halls = db.Halls.ToList();
            if (slot != " ")
            {
                halls = halls.Where(x => x.Event_Slot == slot).ToList();
            }
            if (location != " ")
            {
                halls = halls.Where(x => x.Hall_Location == location).ToList();
            }
            if (pricerange != null)
            {
                halls = halls.Where(x => x.Hall_Price <= pricerange).ToList();
            }
            var seacrhFilter = new SearchFilterData
            {
                Location = location ?? " ",
                Price = pricerange ?? 1000,
                Slot = slot ?? " ",
            };
            Session["searchfilter"] = seacrhFilter;
            TempData["halls"] = halls;

            return RedirectToAction("index");

        }
        

        public ActionResult contact()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Contact(string email, string name, string subject, string message)
        {
            //MailProvider.SentfromMail(email, subject, message);
            //TempData["Text"] = "Thank you for contacting us!!";
            //return View();
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(email);
                mailMessage.To.Add("yousaffsaqlain98@gmail.com");
                mailMessage.Subject = subject;

                mailMessage.Body = "<b>Sender Name : </b>" + name + "<br/>"
                    + "<b>Sender Email : </b>" + email + "<br/>"
                    + "<b>Comments : </b>" + message;
                mailMessage.IsBodyHtml = true;


                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new
                System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["password"]);
                smtpClient.Send(mailMessage);

                TempData["Text"] = "Thank you for contacting us!! ";
            }
            catch
            {
                // Log the exception information to 
                // database table or event viewer

                TempData["Text"] = "There is an unknown Internet connection problem. Please try later!!";
            }

            return View();

        }
        public ActionResult Foodleftover()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Foodleftover(FoodLeftover foodLeftover)
        {
            if (ModelState.IsValid)
            {

                foodLeftover.Date = System.DateTime.Now;

                foodLeftover.Customer_FID = BaseHelper.customer.Customer_ID;

                db.FoodLeftovers.Add(foodLeftover);
                db.SaveChanges();
                TempData["ok"] = "Request Received!!";
            }
            else
            {
                TempData["ok"] = "Login into your account first!!";
                return RedirectToAction("loginuser", "Customers");

            }

            MailProvider.SentfromMail(BaseHelper.customer.Customer_Email, "Request Confirmation ✉", "Your Request has been received and will be contact within 3 working days\n Regards: EMS <br /> Thanks");

            TempData["ok"] = "Your Request has been Sent to \t EMS Admin.\nThanks";
            return RedirectToAction("Foodleftover", "home");
        }
       

        public ActionResult venue(int? id)
        {
            if (id != null)
            {
                ViewData["veuneid"] = id;
            }

            return View();
        }

        public ActionResult VenueDetails(int id)
        {
            var query = db.Venues.Where(x => x.Venue_ID == id).FirstOrDefault();
            return View(query);
        }


        [HttpPost]
        public ActionResult VenueDetails(Feedback feedback)
        {
            if (BaseHelper.customer != null)
            {

                feedback.Feedback_Date = System.DateTime.Now;

                feedback.Customer_FID = BaseHelper.customer.Customer_ID;

                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                TempData["ok"] = "Comment Posted!!";
            }
            else
            {
                TempData["ok"] = "Login into your account!!";
            }
            var query = db.Venues.Where(x => x.Venue_ID == feedback.Venue_FID).FirstOrDefault();

            return View(query);
        }


        public string GetService()
        {
            List<Service> li = db.Services.OrderBy(x => x.Service_ID).ToList();
            var json = JsonConvert.SerializeObject(li);
            return json;

        }


        public ActionResult Services(int? id, int? page, int? hallid)
        {
            if (id != null)
            {
                ViewData["serviceid"] = id;

            }
            if (hallid != null && hallid != 0)
            {
                Session["hallid"] = hallid;

            }
            var pagesize = 10;
            var liststart = db.Services.Where(x => x.Sub_ServiceCategory.Service_Category.ServiceCategory_Name.Contains("Cuisine")).OrderByDescending(x => x.Service_Date).Skip(0).Take(pagesize);
            if (page != null && id == null)
            {
                liststart = db.Services.Where(x => x.Sub_ServiceCategory.Service_Category.ServiceCategory_Name.Contains("Cuisine")).OrderByDescending(x => x.Service_Date).Skip(((int)page - 1) * pagesize).Take(pagesize);
                return View(liststart);
            }
            if (page != null && id != null)
            {
                liststart = db.Services.Where(x => x.SubCategory_FID == id).OrderByDescending(x => x.Service_Date).Skip(((int)page - 1) * pagesize).Take(pagesize);
                return View(liststart);
            }
            return View(liststart);

        }
        //HAll
        public ActionResult Hall(int? id, string filter)
        {

            if (id != null)
            {
                ViewData["hallid"] = id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                ViewData["filter"] = filter;
            }

            return View();

        }
        public ActionResult Hall_Details(int id)
        {
            var query = db.Halls.Where(x => x.Hall_ID == id).FirstOrDefault();
            return View(query);

        }

        [HttpPost]
        public ActionResult Hall_Details(Feedback feedback)
        {
            if (BaseHelper.customer != null)
            {

                feedback.Feedback_Date = System.DateTime.Now;

                feedback.Customer_FID = BaseHelper.customer.Customer_ID;

                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                TempData["ok"] = "Comment Posted!!";
            }
            else
            {
                TempData["ok"] = "Login into your account!!";
            }
            var query = db.Halls.Where(x => x.Hall_ID == feedback.Hall_FID).FirstOrDefault();

            return View(query);

        }
        public ActionResult Service_Details(int id)
        {
            var query = db.Services.Where(x => x.Service_ID == id).FirstOrDefault();
            return View(query);

        }

        [HttpPost]
        public ActionResult Service_Details(Feedback feedback)
        {
            if (BaseHelper.customer != null)
            {

                feedback.Feedback_Date = System.DateTime.Now;

                feedback.Customer_FID = BaseHelper.customer.Customer_ID;

                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                TempData["ok"] = "Comment Posted!!";
            }
            else
            {
                TempData["ok"] = "Login into your account!!";
            }
            var query = db.Services.Where(x => x.Service_ID == feedback.Service_FID).FirstOrDefault();

            return View(query);

        }
       
        public ActionResult ChatRoom()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string tags)
        {
            var art = db.Services.Where(x => x.Service_Name == tags).FirstOrDefault();
            return View(art);
        }



    }
}