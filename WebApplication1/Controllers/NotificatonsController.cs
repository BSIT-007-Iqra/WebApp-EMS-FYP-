using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace ReadRix.Controllers
{
    public class NotificatonsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Notificatons
        public ActionResult Index()
        {
            return View(db.Notifications.Where(x=>x.EventOrganizers_FID == BaseHelper.event_organizers.EventOrganizer_ID).ToList());
        }

       
        public ActionResult addnotification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addnotification(Notification notification)
        {
            notification.DateTime = System.DateTime.Now;
            notification.EventOrganizers_FID = BaseHelper.event_organizers.EventOrganizer_ID;
            
            db.Notifications.Add(notification);
            db.SaveChanges();
            TempData["success"] = "Notification sent!!";
           
            return View();
        }
       
        // GET: Notificatons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notificaton = db.Notifications.Find(id);
            if (notificaton == null)
            {
                return HttpNotFound();
            }
            return View(notificaton);
        }

        // POST: Notificatons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notificaton = db.Notifications.Find(id);
            db.Notifications.Remove(notificaton);
            db.SaveChanges();
            TempData["error"] = "Notification has been deleted successfully";
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
