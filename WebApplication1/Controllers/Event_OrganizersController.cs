using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;
using static Dropbox.Api.TeamLog.SharedLinkAccessLevel;

namespace WebApplication1.Controllers
{
    public class Event_OrganizersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Event_Organizers
        public ActionResult Index()
        {
            var event_Organizers = db.Event_Organizers.Include(e => e.Admin);
            return View(db.Event_Organizers.ToList());
        }

        public ActionResult IndexOrganizer()
        {
            var event_Organizer = db.Event_Organizers.Include(w => w.Admin);
            return View(event_Organizer.ToList());
        }
        // GET: Event_Organizers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Organizers event_Organizers = db.Event_Organizers.Find(id);
            if (event_Organizers == null)
            {
                return HttpNotFound();
            }
            return View(event_Organizers);
        }
        public ActionResult DetailsOrganizer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Organizers event_Organizers = db.Event_Organizers.Find(id);
            if (event_Organizers == null)
            {
                return HttpNotFound();
            }
            return View(event_Organizers);
        }
        public ActionResult activateaccount(int id)
        {
            Event_Organizers event_Organizers = db.Event_Organizers.Where(x => x.EventOrganizer_ID == id).FirstOrDefault();
            event_Organizers.Status = 1;
            if (event_Organizers.Event_Organizer_HireDate == null)
            {
                event_Organizers.Event_Organizer_HireDate = DateTime.UtcNow;

            }
            MailProvider.SentfromMail(event_Organizers.EventOrganizer_Email, "Account Activation Mail!!", "Dear " + event_Organizers.EventOrganizer_Name + "!! Your account has been Activated by Admin. Check out Your account details.<br /> Regards EMS, Thanks!!");
            TempData["success"] = event_Organizers.EventOrganizer_Email + " Account has been Activated";
            db.Entry(event_Organizers).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexOrganizer", "Event_Organizers");
        }

        // GET: Event_Organizers/Create
        public ActionResult Create()
        {
            ViewBag.Admin_FID = new SelectList(db.Admins, "Admin_ID", "Admin_Name");
            return View();
        }

        // POST: Event_Organizers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event_Organizers event_Organizers, HttpPostedFileBase pic)
        {
            string fullpath = Server.MapPath("~/Content/WebsitePicture/" + pic.FileName);
            pic.SaveAs(fullpath);
            event_Organizers.EventOrganizer_Picture = "~/Content/WebsitePicture/" + pic.FileName;
            
            if (ModelState.IsValid)
            {
                db.Event_Organizers.Add(event_Organizers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_FID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", event_Organizers.Admin_FID);
            return View(event_Organizers);
        }

        public ActionResult blockaccount(int id)
        {
            Event_Organizers o = db.Event_Organizers.Where(x => x.EventOrganizer_ID == id).FirstOrDefault();
            o.Status = -1;
            MailProvider.SentfromMail(o.EventOrganizer_Email, "Account Blockage Mail!!", "Dear " + o.EventOrganizer_Name + "!! Your account has been Blocked by Admin. Check out Your account details.<br /> Regards EMS, Thanks!!");
            TempData["success"] = o.EventOrganizer_Email + " Account has been blocked";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexOrganizer", "Event_Organizers");
        }
        // GET: Event_Organizers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Organizers event_Organizers = db.Event_Organizers.Find(id);
            if (event_Organizers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_FID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", event_Organizers.Admin_FID);
            return View(event_Organizers);
        }

        // POST: Event_Organizers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event_Organizers event_Organizers, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    string fullpath = Server.MapPath("~/Content/WebsitePicture/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    event_Organizers.EventOrganizer_Picture = "~/Content/WebsitePicture/" + pic.FileName;
                }

                db.Entry(event_Organizers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_FID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", event_Organizers.Admin_FID);
            return View(event_Organizers);
        }

        // GET: Event_Organizers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Organizers event_Organizers = db.Event_Organizers.Find(id);
            if (event_Organizers == null)
            {
                return HttpNotFound();
            }
            return View(event_Organizers);
        }

        // POST: Event_Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event_Organizers event_Organizers = db.Event_Organizers.Find(id);
            if (ModelState.IsValid)
            {
                event_Organizers.Status = 0;
                db.Event_Organizers.Remove(event_Organizers);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Reset Password
        [HttpPost]
        public ActionResult Forgotpasword(string email)
        {
            var v = db.Event_Organizers.Where(x => x.EventOrganizer_Email == email).FirstOrDefault();
            if (v == null)
            {
                TempData["msg"] = "invalid email";
                return RedirectToAction("login");
            }
            Random random = new Random();
            int code = random.Next(1001, 9999);
            Session["code"] = code;
            Session["userforgotpassword"] = v;
            MailProvider.Sentforgotpassmail(v.EventOrganizer_Email, code);
            return RedirectToAction("CodeVerify");


        }
        public ActionResult CodeVerify()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CodeVerify(int code)
        {
            int sentcode = (int)Session["code"];
            if (code == sentcode)
            {
                TempData["msg"] = "Code validate!! Set your new password";
                return RedirectToAction("NewPassword");
            }
            TempData["error"] = "invalid code. \n Try Again!";
            return View();
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string password)
        {
            var user = (Event_Organizers)Session["userforgotpassword"];
            var v = db.Event_Organizers.Where(x => x.EventOrganizer_Email == user.EventOrganizer_Email).FirstOrDefault();
            v.EventOrganizer_Password = password;
            db.Entry(v).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "Successfully changed password!";
            return RedirectToAction("login");
        }
        // send password to email
        public ActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgotpassword(Event_Organizers event_Organizers)
        {

            Event_Organizers var = db.Event_Organizers.
                Where(x => x.EventOrganizer_Email == event_Organizers.EventOrganizer_Email && 
                x.EventOrganizer_Name == event_Organizers.EventOrganizer_Name).FirstOrDefault();
            if (var != null)
            {
                MailProvider.SentfromMail(var.EventOrganizer_Email, "Fogotton Password!!", "Dear " + event_Organizers.EventOrganizer_Name + "!! , Your Password is  " + var.EventOrganizer_Password + "\n\n\nThanks & Regards\nEMS Team<br /> Thanks");
                TempData["send"] = "Your Password has been sent to Your Registered Email Address.\n Please! Check Your Mail Inbox";
            }
            else
            {
                TempData["send"] = "Either Your Username is Not Valid or Email Not Registered";

            }
            return View();
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Event_Organizers event_Organizers)
        {

            //int v = db.Event_Organizers.Where(x => x.EventOrganizer_Email == event_Organizers.EventOrganizer_Email && event_Organizers.EventOrganizer_Password == event_Organizers.EventOrganizer_Password).Count();
            Event_Organizers var = db.Event_Organizers.Where(x => x.EventOrganizer_Email == event_Organizers.EventOrganizer_Email
            && x.EventOrganizer_Password == event_Organizers.EventOrganizer_Password).FirstOrDefault();
            if (var != null)
            {
                if (var.Status == 1)
                {
                    BaseHelper.event_organizers = var;
                    return RedirectToAction("IndexOrganizer", "home");
                }

                else if (var.Status == -1)
                {
                    ViewBag.message = "Your account has been blocked by admin";
                    return View();
                }
                else if (var.Status == 0)
                {
                    ViewBag.message = "Your account has been deleted.Please send Mail to admin to Re-activate it.";
                    return View();
                }
                else
                {
                    ViewBag.message = "Your account request has not been processed by admin";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Invalid Email Or Password";
                return View();
            }

        }
        public ActionResult logout()
        {
            BaseHelper.event_organizers = null;
            return RedirectToAction("Index", "home");

        }
        public ActionResult SalaryMenu()
        {
            return View();
        }
        public ActionResult ViewReport(DateTime? Todate, DateTime? Fromdate, int? Hall, int? Venue)
        {
            ViewBag.Venue = new SelectList(db.Venues, "Venue_ID", "Venue_Name");
            if (Venue == null)
                ViewBag.Hall = new SelectList(db.Halls, "Hall_ID", "Hall_Name");
            else
                ViewBag.Hall = new SelectList(db.Halls.Where(x => x.Venue_FID == Venue), "Hall_ID", "Hall_Name");

            if (Todate == null)
            {

                Todate = DateTime.Now;
            }

            if (Fromdate == null)
            {

                Fromdate = DateTime.Today;
            }
            ViewBag.Fromdate = Fromdate.Value.ToString("s");
            ViewBag.Todate = Todate.Value.ToString("s");
            var od = db.Booking_Details.Select(x => x.Booking_FID).ToList();
            if (Venue != null)
            {
                od = db.Booking_Details.Where(x => x.Hall.Venue_FID == Venue).Select(x => x.Booking_FID).ToList();
            }
            if (Hall != null)
            {
                od = db.Booking_Details.Where(x => x.Hall_FID == Hall).Select(x => x.Booking_FID).ToList();
            }

            var orderslist = db.Bookings.Where(x => x.Event_Date <= Todate && x.Event_Date >= Fromdate && od.Contains(x.Booking_ID)).ToList();
            return View(orderslist);
        }

    }
}
