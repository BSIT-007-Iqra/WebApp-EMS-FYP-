using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    public class ServicesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Event_Organizers).Include(s => s.Sub_ServiceCategory);
            return View(services.ToList());
        }

        public ActionResult IndexServices()
        {
            var services = db.Services.Include(a => a.Sub_ServiceCategory).Include(a => a.Event_Organizers);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.Organizer_FID = new SelectList(db.Event_Organizers.Where(x => x.EventOrganizer_ID == BaseHelper.event_organizers.EventOrganizer_ID), "EventOrganizer_ID", "EventOrganizer_Name");
            ViewBag.SubCategory_FID = new SelectList(db.Sub_ServiceCategory, "SubCategory_ID", "Sub_category_Name");
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase pic)
        {
            string fullpath = Server.MapPath("~/Content/HallPicture/" + pic.FileName);
            pic.SaveAs(fullpath);
            service.Service_Picture = "~/Content/HallPicture/" + pic.FileName;
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                TempData["success"] = "Service has been Added successfully";
                string notification = service.Service_Name + " Service has been recently started.Checkout our amazing Service.";
                NotificationFunction.SendMessage(notification);
                return RedirectToAction("Index");
            }

            ViewBag.Organizer_FID = new SelectList(db.Event_Organizers, "EventOrganizer_ID", "EventOrganizer_Name", service.Organizer_FID);
            ViewBag.SubCategory_FID = new SelectList(db.Sub_ServiceCategory, "SubCategory_ID", "Sub_category_Name", service.SubCategory_FID);
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name");
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Organizer_FID = new SelectList(db.Event_Organizers, "EventOrganizer_ID", "EventOrganizer_Name", service.Organizer_FID);
            ViewBag.SubCategory_FID = new SelectList(db.Sub_ServiceCategory, "SubCategory_ID", "Sub_category_Name", service.SubCategory_FID);
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name");
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    string fullpath = Server.MapPath("~/Content/HallPicture/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    service.Service_Picture = "~/Content/HallPicture/" + pic.FileName;
                }
                db.Entry(service).State = EntityState.Modified;
            db.SaveChanges();
            TempData["success"] = "Artifact has been edited successfully";
            return RedirectToAction("Index");
            }
            
            ViewBag.Organizer_FID = new SelectList(db.Event_Organizers, "EventOrganizer_ID", "EventOrganizer_Name", service.Organizer_FID);
            ViewBag.Hall_FID = new SelectList(db.Halls, "Hall_ID", "Hall_Name");
            ViewBag.SubCategory_FID = new SelectList(db.Sub_ServiceCategory, "SubCategory_ID", "Sub_category_Name", service.SubCategory_FID);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            TempData["error"] = "Service has been deleted successfully";
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
