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
    public class Website_DetailsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Website_Details
        public ActionResult Index()
        {
            return View(db.Website_Details.ToList());
        }

        // GET: Website_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website_Details website_Details = db.Website_Details.Find(id);
            if (website_Details == null)
            {
                return HttpNotFound();
            }
            return View(website_Details);
        }

        // GET: Website_Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Website_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Website_Details website_Details, HttpPostedFileBase pic)
        {
            string fullpath = Server.MapPath("~/Content/WebsitePicture/" + pic.FileName);
            pic.SaveAs(fullpath);
            website_Details.Website_Logo = "~/Content/WebsitePicture/" + pic.FileName;
            if (ModelState.IsValid)
            {
                db.Website_Details.Add(website_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(website_Details);
        }

        // GET: Website_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website_Details website_Details = db.Website_Details.Find(id);
            if (website_Details == null)
            {
                return HttpNotFound();
            }
            return View(website_Details);
        }

        // POST: Website_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Website_Details website_Details,HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    string fullpath = Server.MapPath("~/Content/WebsitePicture/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    website_Details.Website_Logo = "~/Content/WebsitePicture/" + pic.FileName;
                }

                db.Entry(website_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(website_Details);
        }

        // GET: Website_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website_Details website_Details = db.Website_Details.Find(id);
            if (website_Details == null)
            {
                return HttpNotFound();
            }
            return View(website_Details);
        }

        // POST: Website_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Website_Details website_Details = db.Website_Details.Find(id);
            db.Website_Details.Remove(website_Details);
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
