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
    public class PackagesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Packages
        public ActionResult Index()
        {
            var packages = db.Packages.Include(p => p.Hall).Include(p => p.Service);
            return View(packages.ToList());
        }

        // GET: Packages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name");
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Package_ID,Package_Name,Package_Details,Service_FID,Hall_FID,Packages_Price,Status")] Package package)
        {
            if (ModelState.IsValid)
            {
                package.Per_Head_Price = package.Packages_Price / 100;
                db.Packages.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name", package.Hall_FID);
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name", package.Service_FID);
            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name", package.Hall_FID);
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name", package.Service_FID);
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Package_ID,Package_Name,Package_Details,Service_FID,Hall_FID,Packages_Price,Status")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name", package.Hall_FID);
            ViewBag.Service_FID = new SelectList(db.Services, "Service_ID", "Service_Name", package.Service_FID);
            return View(package);
        }

        // GET: Packages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
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
