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
using WebApplication1.Models;
using WebApplication1.Utils;

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
            //if (BaseHelper.Admin == null)
            //    RedirectToAction("Loginadmin", "Admins");
            //else { 
            //try
            //{
            //    TempData["success"] = "welcome " + BaseHelper.Admin.Admin_Name + " into your dashboard ";

            //}
            //catch (Exception)
            //{
            //   TempData["error"] = "Your Account has not been active right now \n Please contact Admin.";
                
            //}
            //}
            return View();

        }

        public ActionResult about()
        {
            
            return View();
        }
        public ActionResult servicecategory()
        {
            
            return View();
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
        public ActionResult tracking()
        {
           

            return View();
        }
    public ActionResult blog()
        {
           

            return View();
        }
    public ActionResult VenueDetails(int id)
        {
            var query = db.Venues.Where(x => x.Venue_ID == id).FirstOrDefault();
            return View(query);
        }
        
   public ActionResult single()
        {
           

            return View();
        }
   public ActionResult venue(int? id)
        {
            if (id != null)
            {
                ViewData["veuneid"] = id;
           }
            
            return View();
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
        public ActionResult self_caterings()
        {
           

            return View();
        }

    

    
    public ActionResult Services(int? id)
        {
            if (id != null)
            {
                ViewData["serviceid"] = id;
            }

            return View();
        }
        //HAll
        public ActionResult Hall(int? id)
        {

            if (id != null)
            {
                ViewData["hallid"] = id;
            }
            List<Hall> li = db.Halls.OrderBy(x => x.Venue_FID).ToList();

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
    public ActionResult Booking()
        {
           

            return View();
        }
    public ActionResult my_account()
        {
           

            return View();
        }
   
        
        public ActionResult confirmation()
        {
           

            return View();
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
            var art = db.Services.Where(x => x.Service_Name== tags).FirstOrDefault();
            return View(art);
        }



    }
}