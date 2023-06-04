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

namespace WebApplication1.Controllers
{
    public class AdminsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admins
        public ActionResult Index()
        {
            var admins = db.Admins.Include(a => a.Role);
            return View(db.Admins.Where(x => x.Admin_ID == BaseHelper.Admin.Admin_ID).ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            ViewBag.Role_FID = new SelectList(db.Roles, "Role_ID", "Role_Name");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin admin, HttpPostedFileBase pic)
        {
            string fullpath = Server.MapPath("~/Content/AdminPicture/" + pic.FileName);
            pic.SaveAs(fullpath);
            admin.Admin_Picture = "~/Content/AdminPicture/" + pic.FileName;
            admin.Status = "Active";
            if (ModelState.IsValid)
            {

                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role_FID = new SelectList(db.Roles, "Role_ID", "Role_Name", admin.Role_FID);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_FID = new SelectList(db.Roles, "Role_ID", "Role_Name", admin.Role_FID);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin admin, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    string fullpath = Server.MapPath("~/Content/AdminPicture/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    admin.Admin_Picture = "~/Content/AdminPicture/" + pic.FileName;
                }
                admin.Status = "Active";
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }




        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            if (ModelState.IsValid)
            {
                admin.Status = "Active";
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
            }
            BaseHelper.Admin = null;
            return RedirectToAction("Index");
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
            var v = db.Admins.Where(x => x.Admin_Email == email).FirstOrDefault();
            if (v == null)
            {
                TempData["msg"] = "invalid email";
                return RedirectToAction("loginadmin");
            }
            Random random = new Random();
            int code = random.Next(1001, 9999);
            Session["code"] = code;
            Session["userforgotpassword"] = v;
            MailProvider.Sentforgotpassmail(v.Admin_Email, code);
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
            var user = (Admin)Session["userforgotpassword"];
            var v = db.Admins.Where(x => x.Admin_Email == user.Admin_Email).FirstOrDefault();
            v.Admin_Password = password;
            db.Entry(v).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "Successfully changed password!";
            return RedirectToAction("loginadmin");
        }
        // send password to email
        public ActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgotpassword(Admin admin)
        {

            Admin var = db.Admins.Where(x => x.Admin_Email == admin.Admin_Email && x.Admin_Name == admin.Admin_Name).FirstOrDefault();
            if (var != null)
            {
                MailProvider.SentfromMail(var.Admin_Email, "Fogotton Password!!", "Dear " + var.Admin_Name + "!! , Your Password is  " + var.Admin_Password + "\n\n\nThanks & Regards\nEMS Team<br /> Thanks");
                TempData["send"] = "Your Password has been sent to Your Registered Email Address.\n Please! Check Your Mail Inbox";
            }
            else
            {
                TempData["send"] = "Either Your Username is Not Valid or Email Not Registered";

            }
            return View();
        }

        public ActionResult loginadmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult loginadmin(Admin admin)
        {

            Admin var = db.Admins.Where(x => x.Admin_Email == admin.Admin_Email && x.Admin_Password == admin.Admin_Password).FirstOrDefault();
            if (var != null && var.Status == "Active")
            {
                BaseHelper.Admin = var;
                return RedirectToAction("indexadmin", "Home");
            }
            else
            {
                ViewData["msg"] = "Invalid Email and Password";
                return View();
            }

        }
        public ActionResult logout()
        {
            BaseHelper.Admin = null;
            return RedirectToAction("index", "home");

        }
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string body)
        {
            bool mail = MailProvider.SentfromMail(receiver, subject, body);
            if (mail == true)
            { TempData["Msg"] = "Email sent successfully!"; }
            else { TempData["Msg"] = "Error!!"; }
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
