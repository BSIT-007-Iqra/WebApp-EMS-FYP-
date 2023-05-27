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
    public class Event_OrganizersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Event_Organizers
        public ActionResult Index()
        {
            var event_Organizers = db.Event_Organizers.Include(e => e.Admin);
            return View(event_Organizers.ToList());
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
        public ActionResult Create([Bind(Include = "EventOrganizer_ID,EventOrganizer_Name,EventOrganizer_Email,EventOrganizer_Password,EventOrganizer_Contact,EventOrganizer_Gender,EventOrganizer_Picture,Status,Admin_FID")] Event_Organizers event_Organizers)
        {
            if (ModelState.IsValid)
            {
                db.Event_Organizers.Add(event_Organizers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_FID = new SelectList(db.Admins, "Admin_ID", "Admin_Name", event_Organizers.Admin_FID);
            return View(event_Organizers);
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
        public ActionResult Edit([Bind(Include = "EventOrganizer_ID,EventOrganizer_Name,EventOrganizer_Email,EventOrganizer_Password,EventOrganizer_Contact,EventOrganizer_Gender,EventOrganizer_Picture,Status,Admin_FID")] Event_Organizers event_Organizers)
        {
            if (ModelState.IsValid)
            {
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
            db.Event_Organizers.Remove(event_Organizers);
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
