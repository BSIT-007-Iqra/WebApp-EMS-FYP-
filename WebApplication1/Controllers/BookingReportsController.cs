using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class BookingReportsController : Controller
    {
        // GET: Orders_Reports
        Model1 db = new Model1();
        public ActionResult NewBookings()
        {
            var orderslist = db.Bookings.Where(x => x.Booking_Status == "Booked").OrderByDescending(x=>x.Booking_ID).ToList();
            return View(orderslist);
        }
        public ActionResult Invoice(int id)
        {
            var order = db.Bookings.Where(x => x.Booking_ID == id).FirstOrDefault();
            return View(order);
        }
        public ActionResult ProceedBooking(int id)
        {
            var invoicedata = db.Bookings.Find(id);
            invoicedata.Booking_Status = "Proceed";
            db.Entry(invoicedata).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "Your Booking No. " + id + " has been proceeded";
            return RedirectToAction("NewBookings");

        }
        public ActionResult NextProceedBooking()
        {
            var orderslist = db.Bookings.Where(x => x.Booking_Status == "Proceed").OrderByDescending(x => x.Booking_ID).ToList();
            return View(orderslist);
        }

        public ActionResult BookedOrder(int id)
        {
            var invoicedata = db.Bookings.Find(id);
            invoicedata.Booking_Status = "Booked";
            db.Entry(invoicedata).State = EntityState.Modified;
            db.SaveChanges();
            TempData["msg"] = "Your Booking No. " + id + " has been Completed";
            return RedirectToAction("NextProceedBooking");

        }

        //public ActionResult SaleReport(DateTime? Todate , DateTime? Fromdate , int? Service_Category, int? Sub_ServiceCategory, int? Event_Organizer, int? Hall , int? Services )

        //{
        //    ViewBag.Category = new SelectList(db.Service_Category, "ServiceCategory_ID", "ServiceCategory_Name");
        //    ViewBag.Organizer = new SelectList(db.Event_Organizers, "EventOrganizer_ID", "EventOrganizer_Name ");

        //    if (Service_Category == null)
        //    {
        //        ViewBag.SubCategory = new SelectList(db.Sub_ServiceCategory, "SubCategory_ID", "Sub_category_Name");
        //    }
        //    else
        //    {
        //        ViewBag.SubCategory = new SelectList(db.Sub_ServiceCategory.Where(x=>x.Service_Category_FID == Service_Category), "SubCategory_ID", "Sub_category_Name");
        //    }

        //    //if (Event_Organizer == null)
        //    //{
        //    //    ViewBag.hall = new SelectList(db.ShopArtifacts, "ID", "Artifact.ARTIFACT_NAME");
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.Artifact = new SelectList(db.ShopArtifacts.Where(x => x.Artifact.WRITER_FID == Writer ), "ID", "Artifact.ARTIFACT_NAME");

        //    //}
        //    if (Sub_ServiceCategory == null)
        //    {
        //        ViewBag.Artifact1 = new SelectList(db.Services, "Service_ID", "Service_Name");
        //    }
        //    else
        //    {
        //        ViewBag.Artifact1 = new SelectList(db.Services.Where(x=>x.SubCategory_FID == Sub_ServiceCategory), "Service_ID", "Service_Name");
        //    }


        //    if (Todate == null) {

        //        Todate = DateTime.Now;
        //            }

        //    if (Fromdate == null) {

        //        Fromdate = DateTime.Today;
        //    }
        //    ViewBag.Fromdate = Fromdate.Value.ToString("s");
        //    ViewBag.Todate = Todate.Value.ToString("s");
        //    var od = db.Booking_Details.Select(x=>x.Booking_FID).ToList();
        //    if(Service_Category != null) {
        //        var subod = db.Service_Category.Where(x=>x.ServiceCategory_ID== Category).Select(x=>x.ID).ToList();
        //        if(SubCategory != null)
        //        {
        //            subod = db.ShopArtifacts.Where(x => x.Artifact.SUB_CATEGORY_FID == SubCategory).Select(x => x.ID).ToList();
        //        }
        //        if(Artifact1 != null)
        //        {
        //           subod = db.ShopArtifacts.Where(x => x.ID == Artifact1).Select(x=>x.ID).ToList();
        //        }

        //        od = db.Order_Details.Where(x => subod.Contains(x.SHOPARTIFACT_FID)).Select(x=>x.ORDER_FID).ToList();
        //    }

        //    //if(Writer != null) {
        //    //    var subod = db.ShopArtifacts.Where(x=>x.Artifact.WRITER_FID == Writer).Select(x=>x.ID).ToList();

        //    //    if(Artifact != null)
        //    //    {
        //    //       subod = db.ShopArtifacts.Where(x => x.ID == Artifact).Select(x=>x.ID).ToList();
        //    //    }

        //    //    od = db.Order_Details.Where(x => subod.Contains(x.SHOPARTIFACT_FID)).Select(x=>x.ORDER_FID).ToList();
        //    //}


        //    var orderslist = db.Bookings.Where(x => x.Booking_Status == "Booked" && x.Booking_Status == "Booked" && x.Event_Date <= Todate && x.Event_Date >=Fromdate &&  od.Contains(x.Booking_ID)).OrderByDescending(x=>x.Booking_ID).ToList();
        //    return View(orderslist);
        //}


        //public ActionResult ViewReport(DateTime? Todate, DateTime? Fromdate, int? Category, int? SubCategory, int? Writer, int? Artifact, int? Artifact1, int? Episode)

        //{
        //    ViewBag.Category = new SelectList(db.Categories, "CATEGORY_ID", "CATEGORY_NAME");
        //    ViewBag.Writer = new SelectList(db.Writers, "WRITER_ID", "WRITER_NAME");

        //    if (Category == null)
        //    {
        //        ViewBag.SubCategory = new SelectList(db.SubCategories, "SUB_CATEGORY_ID", "SUB_CATEGORY_NAME");

        //    }
        //    else
        //    {
        //        ViewBag.SubCategory = new SelectList(db.SubCategories.Where(x => x.CATEGORY_FID == Category), "SUB_CATEGORY_ID", "SUB_CATEGORY_NAME");

        //    }

        //    if (Writer == null)
        //    {
        //        ViewBag.Artifact = new SelectList(db.Artifacts, "ARTIFACT_ID", "ARTIFACT_NAME");
        //    }
        //    else
        //    {
        //        ViewBag.Artifact = new SelectList(db.Artifacts.Where(x => x.WRITER_FID == Writer), "ARTIFACT_ID", "ARTIFACT_NAME");

        //    }
        //    if (SubCategory == null)
        //    {
        //        ViewBag.Artifact1 = new SelectList(db.Artifacts, "ARTIFACT_ID", "ARTIFACT_NAME");
        //    }
        //    else
        //    {
        //        ViewBag.Artifact1 = new SelectList(db.Artifacts.Where(x => x.SUB_CATEGORY_FID == SubCategory), "ARTIFACT_ID", "ARTIFACT_NAME");
        //    }
        //     if (Artifact1 == null)
        //    {
        //        ViewBag.Episode = new SelectList(db.Episodes, "EPISODE_ID", "EPISODE_NO");
        //    }
        //    else
        //    {
        //        ViewBag.Episode = new SelectList(db.Episodes.Where(x => x.ARTIFACT_FID  == Artifact1 ), "EPISODE_ID", "EPISODE_NO");
        //    }


        //    if (Todate == null)
        //    {

        //        Todate = DateTime.Now;
        //    }

        //    if (Fromdate == null)
        //    {

        //        Fromdate = DateTime.Today;
        //    }
        //    ViewBag.Fromdate = Fromdate.Value.ToString("s");
        //    ViewBag.Todate = Todate.Value.ToString("s");
        //    var od = db.Views.Select(x => x.VIEW_ID).ToList();
        //    if (Category != null)
        //    {
        //        var subod = db.Episodes.Where(x => x.Artifact.SubCategory.CATEGORY_FID == Category).Select(x => x.EPISODE_ID).ToList();
        //        if (SubCategory != null)
        //        {
        //            subod = db.Episodes.Where(x => x.Artifact.SUB_CATEGORY_FID == SubCategory).Select(x => x.EPISODE_ID).ToList();
        //        }
        //        if (Artifact1 != null)
        //        {
        //            subod = db.Episodes.Where(x => x.ARTIFACT_FID == Artifact1).Select(x => x.EPISODE_ID).ToList();
        //        }
        //         if (Episode != null)
        //        {
        //            subod = db.Episodes.Where(x => x.EPISODE_ID == Episode).Select(x => x.EPISODE_ID).ToList();
        //        }

        //        od = db.Views.Where(x => subod.Contains(x.EPISODE_FID)).Select(x => x.VIEW_ID).ToList();
        //    }

        //    if (Writer != null)
        //    {
        //        var subod = db.Episodes.Where(x => x.Artifact.Writer.WRITER_ID == Writer).Select(x => x.EPISODE_ID).ToList();

        //        if (Artifact != null)
        //        {
        //            subod = db.Episodes.Where(x => x.ARTIFACT_FID == Artifact).Select(x => x.EPISODE_ID).ToList();
        //        }

        //        od = db.Views.Where(x => subod.Contains(x.EPISODE_FID)).Select(x => x.VIEW_ID).ToList();
        //    }


        //    var orderslist = db.Views.Where(  x=>x.VIEW_DATE <= Todate && x.VIEW_DATE >= Fromdate && od.Contains(x.VIEW_ID)).ToList();


        //    return View(orderslist);
        //}



    }

}