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
    public class HallsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Halls
        public ActionResult Index()
        {
            var halls = db.Halls.Include(h => h.Venue);
            return View(halls.ToList());
        }

        // GET: Halls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }

        // GET: Halls/Create
        public ActionResult Create()
        {
            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name");
            return View();
        }

        // POST: Halls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hall_ID,Hall_Name,Hall_Description,Hall_Picture,Hall_Picture1,Hall_Picture2,Hall_Picture3,Hall_Price,Event_Slot,Event_Time_Slot,Hall_Location,Staff,Ameities,Cancellation_Policy,Venue_Type,Venue_FID")] Hall hall)
        {
            if (ModelState.IsValid)
            {
                db.Halls.Add(hall);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", hall.Venue_FID);
            return View(hall);
        }

        // GET: Halls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", hall.Venue_FID);
            return View(hall);
        }

        // POST: Halls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hall_ID,Hall_Name,Hall_Description,Hall_Picture,Hall_Picture1,Hall_Picture2,Hall_Picture3,Hall_Price,Event_Slot,Event_Time_Slot,Hall_Location,Staff,Ameities,Cancellation_Policy,Venue_Type,Venue_FID")] Hall hall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hall).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Venue_FID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", hall.Venue_FID);
            return View(hall);
        }

        // GET: Halls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }

        // POST: Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hall hall = db.Halls.Find(id);
            db.Halls.Remove(hall);
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
