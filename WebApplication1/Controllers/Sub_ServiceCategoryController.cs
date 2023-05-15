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
    public class Sub_ServiceCategoryController : Controller
    {
        private Model1 db = new Model1();

        // GET: Sub_ServiceCategory
        public ActionResult Index()
        {
            var sub_ServiceCategory = db.Sub_ServiceCategory.Include(s => s.Service_Category);
            return View(sub_ServiceCategory.ToList());
        }

        // GET: Sub_ServiceCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_ServiceCategory sub_ServiceCategory = db.Sub_ServiceCategory.Find(id);
            if (sub_ServiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(sub_ServiceCategory);
        }

        // GET: Sub_ServiceCategory/Create
        public ActionResult Create()
        {
            ViewBag.Service_Category_FID = new SelectList(db.Service_Category, "ServiceCategory_ID", "ServiceCategory_Name");
            return View();
        }

        // POST: Sub_ServiceCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategory_ID,Sub_category_Name,Service_Category_FID")] Sub_ServiceCategory sub_ServiceCategory)
        {
            if (ModelState.IsValid)
            {
                db.Sub_ServiceCategory.Add(sub_ServiceCategory);
                db.SaveChanges();
                TempData["success"] = "SubCategory has been added";
                return RedirectToAction("Index");
            }

            ViewBag.Service_Category_FID = new SelectList(db.Service_Category, "ServiceCategory_ID", "ServiceCategory_Name", sub_ServiceCategory.Service_Category_FID);
            return View(sub_ServiceCategory);
        }

        // GET: Sub_ServiceCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_ServiceCategory sub_ServiceCategory = db.Sub_ServiceCategory.Find(id);
            if (sub_ServiceCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Service_Category_FID = new SelectList(db.Service_Category, "ServiceCategory_ID", "ServiceCategory_Name", sub_ServiceCategory.Service_Category_FID);
            return View(sub_ServiceCategory);
        }

        // POST: Sub_ServiceCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategory_ID,Sub_category_Name,Service_Category_FID")] Sub_ServiceCategory sub_ServiceCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_ServiceCategory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "SubCategory has been edited successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Service_Category_FID = new SelectList(db.Service_Category, "ServiceCategory_ID", "ServiceCategory_Name", sub_ServiceCategory.Service_Category_FID);
            return View(sub_ServiceCategory);
        }

        // GET: Sub_ServiceCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_ServiceCategory sub_ServiceCategory = db.Sub_ServiceCategory.Find(id);
            if (sub_ServiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(sub_ServiceCategory);
        }

        // POST: Sub_ServiceCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_ServiceCategory sub_ServiceCategory = db.Sub_ServiceCategory.Find(id);
            db.Sub_ServiceCategory.Remove(sub_ServiceCategory);
           TempData["error"] = "SubCategory has been Deleted";
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
