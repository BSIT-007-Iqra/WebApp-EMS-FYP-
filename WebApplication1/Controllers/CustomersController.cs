using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;
using static Dropbox.Api.Files.ListRevisionsMode;
using static Dropbox.Api.TeamLog.SharedLinkAccessLevel;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.Where(x => x.Customer_ID == BaseHelper.customer.Customer_ID).ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer, HttpPostedFileBase pic)
        {
            if (!db.Customers.Any(x => x.Customer_Email == customer.Customer_Email))
            {

                string fullpath = Server.MapPath("~/Content/AdminPicture/" + pic.FileName);
                pic.SaveAs(fullpath);
                customer.Customer_Picture = "~/Content/AdminPicture/" + pic.FileName;
                customer.IS_ACCOUNT_ACTIVE = true;
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();

                }
                TempData["ok"] = "Your Account has been successfully Created!!";
            }
            else
            {
                TempData["err"] = "Email already exists!!";
            }
            return RedirectToAction("loginuser", "Customers");

        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer, HttpPostedFileBase pic)
        {
            //if (!db.Readers.Any(x => x.READER_EMAIL == reader.READER_EMAIL))
            //{
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    string fullpath = Server.MapPath("~/Content/AdminPicture/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    customer.Customer_Picture = "~/Content/AdminPicture/" + pic.FileName;
                }

                customer.IS_ACCOUNT_ACTIVE = true;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (ModelState.IsValid)
            {
                customer.IS_ACCOUNT_ACTIVE = false;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
            }
            BaseHelper.customer = null;
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
        [HttpPost]
        public ActionResult Forgotpasword(string email)
        {
            var v = db.Customers.Where(x => x.Customer_Email == email).FirstOrDefault();
            if (v == null)
            {
                TempData["msg"] = "invalid email";
                return RedirectToAction("loginuser");
            }
            Random random = new Random();
            int code = random.Next(1001, 9999);
            Session["code"] = code;
            Session["userforgotpassword"] = v;
            MailProvider.Sentforgotpassmail(v.Customer_Email, code);
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
            TempData["error"] = "invalid code";
            return View();
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string password)
        {
            var user = (Customer)Session["userforgotpassword"];
            var v = db.Customers.Where(x => x.Customer_Email == user.Customer_Email).FirstOrDefault();
            v.Customer_Password = password;
            db.Entry(v).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "Successfully changed password!!";
            return RedirectToAction("loginuser");
        }
        //Send forgor password on email
        public ActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgotpassword(Customer reader)
        {

            Customer v = db.Customers.Where(x => x.Customer_Email == reader.Customer_Email && x.Customer_Name == reader.Customer_Name).FirstOrDefault();
            if (v != null)
            {
                MailProvider.SentfromMail(v.Customer_Email, "Fogotton Password!!", "Dear " + v.Customer_Name + "!! , Your Password is  " + v.Customer_Password + "\n\n\nThanks & Regards\nReadRix Team<br /> Thanks");

                TempData["send"] = "Your Password Has Been Sent to Registered Email Address. Check Your Mail Inbox";
            }
            else
            {
                TempData["send"] = "Your Username is Not Valid or Email Not Registered";

            }

            return View();
        }


        public ActionResult loginuser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult loginuser(Customer reader)
        {

            Customer v = db.Customers.Where(x => x.Customer_Email == reader.Customer_Email && x.Customer_Password == reader.Customer_Password).FirstOrDefault();

            if (v != null)
            {
                if (v.IS_ACCOUNT_ACTIVE == true)
                {
                    BaseHelper.customer = v;

                    return RedirectToAction("Displaybooking", "Cart");


                }
                else
                {
                    ViewBag.message = "Your Prevoius Account has been deleted. please Re-Create New!!";
                    return View();
                }

            }
            else
            {
                ViewBag.message = "Invalid email and password";
                return View();
            }

        }
        public ActionResult logout()
        {
            BaseHelper.customer = null;
            return RedirectToAction("index", "home");

        }

        public ActionResult CustomerHistory()
        {
            var order = db.Bookings.Where(x => x.Booking_Status == "Booked" && x.Customer_FID == BaseHelper.customer.Customer_ID).ToList();
            return View(order);

        }
        public ActionResult Invoice(int id)
        {
            var order = db.Bookings.Where(x => x.Booking_ID == id).FirstOrDefault();
            return View(order);
        }
        public ActionResult CancelledBookings()
        {
            var order = db.Bookings.Where(x => x.Booking_Status == "Cancelled" && x.Customer_FID == BaseHelper.customer.Customer_ID).ToList();
            return View(order);
        }
        public ActionResult CancelBookings(int id)
        {
            Booking order = db.Bookings.Where(x => x.Booking_ID == id).FirstOrDefault();
            order.Booking_Status = "Cancelled";


            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CancelledBookings");
        }
        public ActionResult ActiveBookings(int id)
        {
            Booking order = db.Bookings.Where(x => x.Booking_ID == id).FirstOrDefault();
            order.Booking_Status = "Booked";
            order.Event_Date = DateTime.Now;

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CustomerHistory");
        }
    }
}