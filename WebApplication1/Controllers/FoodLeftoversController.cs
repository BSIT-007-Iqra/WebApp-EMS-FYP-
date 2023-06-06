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
    public class FoodLeftoversController : Controller
    {
        private Model1 db = new Model1();

        // GET: FoodLeftovers
        public ActionResult Index()
        {
            var foodLeftovers = db.FoodLeftovers.Include(f => f.Customer);
            return View(foodLeftovers.ToList());
        }

        // GET: FoodLeftovers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodLeftover foodLeftover = db.FoodLeftovers.Find(id);
            if (foodLeftover == null)
            {
                return HttpNotFound();
            }
            return View(foodLeftover);
        }

        // GET: FoodLeftovers/Create
        public ActionResult Create()
        {
            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name");
            return View();
        }

        // POST: FoodLeftovers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodRequest_ID,ID,Reason,Date,Status,Customer_FID")] FoodLeftover foodLeftover)
        {
            if (ModelState.IsValid)
            {
                db.FoodLeftovers.Add(foodLeftover);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", foodLeftover.Customer_FID);
            return View(foodLeftover);
        }

        // GET: FoodLeftovers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodLeftover foodLeftover = db.FoodLeftovers.Find(id);
            if (foodLeftover == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", foodLeftover.Customer_FID);
            return View(foodLeftover);
        }

        // POST: FoodLeftovers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodRequest_ID,ID,Reason,Date,Status,Customer_FID")] FoodLeftover foodLeftover)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodLeftover).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_FID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", foodLeftover.Customer_FID);
            return View(foodLeftover);
        }

        // GET: FoodLeftovers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodLeftover foodLeftover = db.FoodLeftovers.Find(id);
            if (foodLeftover == null)
            {
                return HttpNotFound();
            }
            return View(foodLeftover);
        }

        // POST: FoodLeftovers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodLeftover foodLeftover = db.FoodLeftovers.Find(id);
            db.FoodLeftovers.Remove(foodLeftover);
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
