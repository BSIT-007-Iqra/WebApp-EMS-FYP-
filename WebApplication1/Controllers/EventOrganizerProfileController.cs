using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Views.EventOganizerProfile
{
    public class EventOrganizerProfileController : Controller
    {
        Model1 db = new Model1();
        // GET: AdminProfile
        public ActionResult ProfileIndex()
        {
            return View(db.Event_Organizers.Where(x => x.EventOrganizer_ID == BaseHelper.event_organizers.EventOrganizer_ID).ToList());

        }

        // GET: Admins/Details/5
        public ActionResult ProfileDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Organizers eventorganizer = db.Event_Organizers.Find(id);
            if (eventorganizer == null)
            {
                return HttpNotFound();
            }
            return View(eventorganizer);
        }
        // GET: Admins/Edit/5
        public ActionResult ProfileEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Organizers eventorganizer = db.Event_Organizers.Find(id);
            if (eventorganizer == null)
            {
                return HttpNotFound();
            }
            return View(eventorganizer);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEdit(Event_Organizers eventorganizer, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    string fullpath = Server.MapPath("~/Content/AdminPicture/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    eventorganizer.EventOrganizer_Picture = "~/Content/AdminPicture/" + pic.FileName;
                }
                eventorganizer.Status = 1;
                db.Entry(eventorganizer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProfileIndex");
            }
            return View(eventorganizer);
        }
    }
}