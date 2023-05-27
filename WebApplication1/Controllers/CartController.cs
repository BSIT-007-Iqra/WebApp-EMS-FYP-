using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;
using static Dropbox.Api.Files.ListRevisionsMode;

namespace ReadRix.Controllers
{
    public class CartController : Controller
    {
        Model1 db = new Model1();
        // GET: Cart
        public ActionResult Displaybooking()
        {
            if (BaseHelper.customer == null)
            {
                TempData["new"] = "Login into your account OR Register Account";
            }
            return View();
        }

        public ActionResult Bookingdate(Booking booking)
        {
            //var b = db.Bookings.Where(x => x.Event_Date == booking.Event_Date).FirstOrDefault();
            if (booking.Event_Date != null)
            {
                TempData["Error"] = "This Date is not available \n please!  Setect another date \t thanks";
            }
            return View();
        }
        
        public ActionResult BookingComplete()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Orderbooked(Booking order)
        {
            int dollerrate = (int)MailProvider.GetCurrentDollorprice();
            if (BaseHelper.customer != null)
            {
                order.Customer_FID = BaseHelper.customer.Customer_ID;
                    order.Booking_Status = "Booked";
                    order.Event_Date = System.DateTime.Now;
                   
                if (order.Payment_Type == "CashOnBooking")
                {
                     order.Booking_Type = "Book";
                    db.Bookings.Add(order);
                    db.SaveChanges();

                    Booking_Details od = new Booking_Details();
                    foreach (var item in (List<Hall>)Session["Cart"])
                    {
                        od.Price = item.Hall_Price;
                        od.Hall_FID = item.Hall_ID;
                        od.Booking_FID = order.Booking_ID;

                        db.Booking_Details.Add(od);
                        db.SaveChanges();

                    }

                    foreach (var item in (List<Service>)Session["Cart1"])
                    {
                        od.Price = item.Service_Price;
                        od.Service_FID = item.Service_ID;
                        od.Booking_FID = order.Booking_ID;
                        db.Booking_Details.Add(od);
                        db.SaveChanges();

                    }
                    MailProvider.SentfromMail(BaseHelper.customer.Customer_Email, "Order Confirmation", "Your Order has been booked and will be delivered within 3 working days, Regards EMS <br /> Thanks");
                    TempData["Cart"] = Session["Cart"];
                    Session["Cart"] = null;
                    TempData["Cart1"] = Session["Cart1"];
                    Session["Cart1"] = null;
                  

                    return RedirectToAction("BookingComplete");
                }
                else {
                    order.Booking_Type = "Proceed";
                    Session["order"] = order;
                    return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=iqrabajwa00@gmail.com&item_name=EMS&return=https://localhost:44373/Cart/PaypalOrderbooked&amount=" + (double.Parse(Session["Amount"].ToString()) / dollerrate));

                }

            }
            else
            {
                TempData["new"] = "Login into your account OR Register Account";
                return RedirectToAction("loginuser", "Customers");
            }


        }

        public ActionResult PaypalOrderbooked()
        {
            Booking o = (Booking)Session["order"];
            db.Bookings.Add(o);
            db.SaveChanges();
            Booking_Details od = new Booking_Details();
            foreach (var item in (List<Hall>)Session["cart"])
            {
                od.Price = item.Hall_Price;
                od.Hall_FID = item.Hall_ID;
                od.Booking_FID = o.Booking_ID;

                db.Booking_Details.Add(od);
                db.SaveChanges();

            }

            foreach (var item in (List<Service>)Session["cart1"])
            {
                od.Price = item.Service_Price;
                od.Service_FID = item.Service_ID;
                od.Booking_FID = o.Booking_ID;
                db.Booking_Details.Add(od);
                db.SaveChanges();

            }


            MailProvider.SentfromMail(BaseHelper.customer.Customer_Email, "Booking Confirmation", "Your Booking has been booked and will Inform you forward through Email, Regards EMS <br /> Thanks");
            TempData["cart"] = Session["cart"];
            Session["cart"] = null;
            TempData["cart"] = Session["cart1"];
            Session["cart1"] = null;
            return RedirectToAction("BookingComplete");

        }

        public ActionResult Addtobooking(int id)
        {
            
            List<Hall> HallList = new List<Hall>();
            //if (Session["cart"] != null)
            //{
            //    HallList = (List<Hall>)Session["cart"];
            //}
            HallList.Where(x => x.Hall_ID == id).FirstOrDefault();
            Hall hall = db.Halls.Where(x => x.Hall_ID == id).FirstOrDefault();
            HallList.Add(hall);
            Session["Cart"] = HallList;
            return RedirectToAction("Displaybooking");

        }
        public ActionResult Addtobooking1(int id)
        {
            List<Service> ServiceList = new List<Service>();
            if (Session["cart1"] != null)
            {
                ServiceList = (List<Service>)Session["cart1"];
            }
            Service existingproduct = ServiceList.Where(x => x.Service_ID == id).FirstOrDefault();
            if (existingproduct != null)
            {

            }
            else
            {
                Service service = db.Services.Where(x => x.Service_ID == id).FirstOrDefault();
                ServiceList.Add(service);
                Session["Cart1"] = ServiceList;

            }
            return RedirectToAction("Displaybooking");

        }

        //public actionresult plustocart(int id)
        //{
        //    list<shopartifact> tblartifactlist = new list<shopartifact>();
        //    
        //    tblartifactlist[id].quantity++;
        //    session["cart"] = tblartifactlist;
        //    return redirecttoaction("displaycart");

        //}
        //public actionresult minusfromcart(int id)
        //{
        //    list<shopartifact> tblartifactlist = new list<shopartifact>();
        //    if (session["cart"] != null)
        //    {
        //        tblartifactlist = (list<shopartifact>)session["cart"];
        //    }
        //    tblartifactlist[id].quantity--;
        //    if (tblartifactlist[id].quantity < 0)
        //    {
        //        tblartifactlist.removeat(id);
        //    }
        //    session["cart"] = tblartifactlist;

        //    return redirecttoaction("displaycart");
        //}

        public ActionResult Removefromcart(int id)
        {
            List<Hall> hallList = new List<Hall>();
            if (Session["Cart"] != null)
            {
                hallList = (List<Hall>)Session["Cart"];
            }

            hallList.RemoveAt(id);
            Session["Cart"] = hallList;
            return RedirectToAction("Displaybooking");
        }
        public ActionResult Removefromcart1(int id)
        {
            List<Service> ServiceList = new List<Service>();
            if (Session["Cart1"] != null)
            {
                ServiceList = (List<Service>)Session["Cart1"];
            }

            ServiceList.RemoveAt(id);
            Session["Cart1"] = ServiceList;
            return RedirectToAction("Displaybooking");
        }
        public ActionResult Closeboking()
        {

            Session["cart"] = null;
            return RedirectToAction("index", "Home");
        }
    }
}