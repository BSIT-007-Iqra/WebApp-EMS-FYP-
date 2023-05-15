using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Views.AdminProfile
{
    public class AdminProfileController : Controller
    {Model1 db = new Model1();
        // GET: AdminProfile
        public ActionResult ProfileIndex()
        {
             return View(db.Admins.Where(x => x.Admin_ID == BaseHelper.Admin.Admin_ID).ToList());
           
        }

        // GET: Admins/Details/5
        public ActionResult ProfileDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        // GET: Admins/Edit/5
        public ActionResult ProfileEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_FID = new SelectList(db.Roles, "Role_ID", "Role_Name", admin.Role_FID);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEdit(Admin admin, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null)
                {
                    string fullpath = Server.MapPath("~/Content/AdminPicture/" + pic.FileName);
                    pic.SaveAs(fullpath);
                    admin.Admin_Picture = "~/Content/AdminPicture/" + pic.FileName;
                }

                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProfileIndex","AdminProfile");
            }
            return View(admin);
        }
    }
}