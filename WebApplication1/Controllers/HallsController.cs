using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using static Dropbox.Api.TeamLog.ClassificationType;

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
        public ActionResult Create(Hall hall, HttpPostedFileBase pic, HttpPostedFileBase pic1, HttpPostedFileBase pic2, HttpPostedFileBase pic3)
        {
            if (pic == null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic.FileName);
                pic.SaveAs(fullpath);
                hall.Hall_Picture = "~/Content/HallPicture/" + pic.FileName;
            }
            if (pic1 == null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic1.FileName);
                pic1.SaveAs(fullpath);
                hall.Hall_Picture1 = "~/Content/HallPicture/" + pic1.FileName;
            }
            if (pic2 == null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic2.FileName);
                pic2.SaveAs(fullpath);
                hall.Hall_Picture2 = "~/Content/HallPicture/" + pic2.FileName;
            }
            if (pic3 == null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic3.FileName);
                pic3.SaveAs(fullpath);
                hall.Hall_Picture3 = "~/Content/HallPicture/" + pic3.FileName;
            }

            if (ModelState.IsValid)
            {
                hall.Per_Head_Price = hall.Hall_Price / 100;
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
        public ActionResult Edit(Hall hall, HttpPostedFileBase pic, HttpPostedFileBase pic1, HttpPostedFileBase pic2, HttpPostedFileBase pic3)
        {
            if (pic != null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic.FileName);
                pic.SaveAs(fullpath);
                hall.Hall_Picture = "~/Content/HallPicture/" + pic.FileName;
            }
            if (pic1 != null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic1.FileName);
                pic1.SaveAs(fullpath);
                hall.Hall_Picture1 = "~/Content/HallPicture/" + pic1.FileName;
            }
            if (pic2 != null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic2.FileName);
                pic2.SaveAs(fullpath);
                hall.Hall_Picture2 = "~/Content/HallPicture/" + pic2.FileName;
            }
            if (pic3 != null)
            {
                string fullpath = Server.MapPath("~/Content/HallPicture/" + pic3.FileName);
                pic3.SaveAs(fullpath);
                hall.Hall_Picture3 = "~/Content/HallPicture/" + pic3.FileName;
            }
            if (ModelState.IsValid)
            {
                hall.Per_Head_Price = hall.Hall_Price / 100;
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
