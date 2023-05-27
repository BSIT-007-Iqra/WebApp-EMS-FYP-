using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FeedbacksController : Controller
    {
        private Model1 db = new Model1();

        // GET: Feedbacks
        public ActionResult Index()
        {
            var feedbacks = db.Feedbacks.Include(f => f.Customer).Include(f => f.Feedback2).Include(f => f.Hall).Include(f => f.Service).Include(f => f.Venue);
            return View(feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name");
            ViewBag.Feedback_FID = new SelectList(db.Feedbacks, "Feedback_ID", "Feedback_Description");
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name");
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name");
            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Feedback_ID,Feedback_Description,Feedback_Date,Customer_FID,Venue_FID,Service_FID,Hall_FID,Feedback_FID")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", feedback.Customer_FID);
            ViewBag.Feedback_FID = new SelectList(db.Feedbacks, "Feedback_ID", "Feedback_Description", feedback.Feedback_FID);
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name", feedback.Hall_FID);
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name", feedback.Service_FID);
            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", feedback.Venue_FID);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", feedback.Customer_FID);
            ViewBag.Feedback_FID = new SelectList(db.Feedbacks, "Feedback_ID", "Feedback_Description", feedback.Feedback_FID);
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name", feedback.Hall_FID);
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name", feedback.Service_FID);
            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", feedback.Venue_FID);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Feedback_ID,Feedback_Description,Feedback_Date,Customer_FID,Venue_FID,Service_FID,Hall_FID,Feedback_FID")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", feedback.Customer_FID);
            ViewBag.Feedback_FID = new SelectList(db.Feedbacks, "Feedback_ID", "Feedback_Description", feedback.Feedback_FID);
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name", feedback.Hall_FID);
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name", feedback.Service_FID);
            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", feedback.Venue_FID);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
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
    }
}
