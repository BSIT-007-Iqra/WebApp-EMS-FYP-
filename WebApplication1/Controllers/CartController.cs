using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;
using static Dropbox.Api.Files.ListRevisionsMode;
using static Dropbox.Api.TeamPolicies.SmartSyncPolicy;

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

        public ActionResult Bookingdate(DateTime eventDate, TimeSpan starttime, TimeSpan endtime, string Event_Name)
        {
            if (Event_Name == " ")
            {
                TempData["ErrorDate"] = " \n Please!  Setect  Event  \t thanks";
                return RedirectToAction("Displaybooking", "Cart");
            }
            bool confirm;
            var query = db.Bookings.Where(x => x.Event_Date == eventDate);

            if (query != null)
            {
                bool isOverlap = false;

                foreach (var item in query)
                {
                    if (starttime >= item.Event_Start_Time && starttime <= item.Event_End_Time)
                    {
                        // Overlapping start timing found
                        isOverlap = true;
                        break;
                    }

                    if (endtime >= item.Event_Start_Time && endtime <= item.Event_End_Time)
                    {
                        // Overlapping end timing found
                        isOverlap = true;
                        break;
                    }
                }
                if (isOverlap)
                {
                    confirm = false;
                    var dates = new EventDates
                    {
                        Event_Date = eventDate,
                        Start_Time = starttime,
                        End_Time = endtime,
                        Confirm = confirm,
                        Event_Name = Event_Name
                    };
                    Session["dates"] = dates;
                    TempData["ErrorDate"] = "These timing are not available \n please!  Setect another date or timing of the day. Thanks";
               
                }
                else
                {
                    TempData["ErrorDate"] = "These timing are available \n Please Proceed Next.Thanks";
                    confirm = true;
                    var dates = new EventDates
                    {
                        Event_Date = eventDate,
                        Start_Time = starttime,
                        End_Time = endtime,
                        Confirm = confirm,
                        Event_Name = Event_Name
                    };
                    Session["dates"] = dates;

                }
            }
            else
            {
                TempData["ErrorDate"] = "These timing are available \n Please Proceed Next.Thanks";
                confirm = true;
                var dates = new EventDates
                {
                    Event_Date = eventDate,
                    Start_Time = starttime,
                    End_Time = endtime,
                    Confirm = confirm,
                    Event_Name = Event_Name
                };
                Session["dates"] = dates;

            }
            return RedirectToAction("Displaybooking", "Cart");
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

                #region EventDates

                var dates = (EventDates)Session["dates"];

                order.Event_Start_Time = dates.Start_Time;
                order.Event_End_Time = dates.End_Time;
                order.Event_Date = dates.Event_Date;
                order.Event_Name = dates.Event_Name;

                #endregion


                if (order.Payment_Type == "CashOnBooking")
                {
                    order.Booking_Type = "Book";

                    db.Bookings.Add(order);
                    db.SaveChanges();

                    Booking_Details od = new Booking_Details();
                    if (Session["cart"] != null)
                    {
                        foreach (var item in (List<Hall>)Session["Cart"])
                        {
                            od.Price = item.Hall_Price;
                            od.Hall_FID = item.Hall_ID;
                            od.Booking_FID = order.Booking_ID;
                            od.No_Of_People = item.Quantity;
                            db.Booking_Details.Add(od);
                            db.SaveChanges();

                        }
                    }
                    if (Session["cart1"] != null)
                    {
                        foreach (var item in (List<Service>)Session["cart1"])
                        {
                            od.Price = item.Service_Price;
                            od.Service_FID = item.Service_ID;
                            od.Booking_FID = order.Booking_ID;

                            db.Booking_Details.Add(od);
                            db.SaveChanges();

                        }

                    }
                    if (Session["Cart2"] != null)
                    {
                        foreach (var item in (List<Package>)Session["Cart2"])
                        {
                            od.Price = item.Packages_Price;
                            od.Package_FID = item.Package_ID;
                            od.Booking_FID = order.Booking_ID;
                            od.No_Of_People = item.Quantity;
                            db.Booking_Details.Add(od);
                            db.SaveChanges();

                        }
                    }
                    MailProvider.SentfromMail(BaseHelper.customer.Customer_Email, "Booking Confirmation", "Your Order has been booked and will be delivered within 3 working days, Regards EMS <br /> Thanks");
                    TempData["Cart"] = Session["Cart"];
                    Session["Cart"] = null;
                    TempData["Cart1"] = Session["Cart1"];
                    Session["Cart1"] = null;
                    TempData["Cart2"] = Session["Cart2"];
                    Session["Cart2"] = null;

                    TempData["dates"] = Session["dates"];
                    Session["dates"] = null;
                    return RedirectToAction("BookingComplete");
                }
                else
                {
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
            if (Session["cart"] != null)
            {
                foreach (var item in (List<Hall>)Session["Cart"])
                {
                    od.Price = item.Hall_Price;
                    od.Hall_FID = item.Hall_ID;
                    od.Booking_FID = o.Booking_ID;
                    od.No_Of_People = item.Quantity;
                    db.Booking_Details.Add(od);
                    db.SaveChanges();

                }
            }
            if (Session["cart1"] != null)
            {
                foreach (var item in (List<Service>)Session["cart1"])
                {
                    od.Price = item.Service_Price;
                    od.Service_FID = item.Service_ID;
                    od.Booking_FID = o.Booking_ID;

                    db.Booking_Details.Add(od);
                    db.SaveChanges();

                }

            }
            if (Session["Cart2"] != null)
            {
                foreach (var item in (List<Package>)Session["Cart2"])
                {
                    od.Price = item.Packages_Price;
                    od.Package_FID = item.Package_ID;
                    od.Booking_FID = o.Booking_ID;
                    od.No_Of_People = item.Quantity;
                    db.Booking_Details.Add(od);
                    db.SaveChanges();

                }
            }
            MailProvider.SentfromMail(BaseHelper.customer.Customer_Email, "Booking Confirmation", "Your Booking has been booked and you will Inform forward through Email, Regards EMS <br /> Thanks");
            TempData["Cart"] = Session["Cart"];
            Session["Cart"] = null;
            TempData["Cart1"] = Session["Cart1"];
            Session["Cart1"] = null;
            TempData["Cart2"] = Session["Cart2"];
            Session["Cart2"] = null;

            TempData["dates"] = Session["dates"];
            Session["dates"] = null;
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
        public ActionResult Addtobooking2(int id)
        {
            List<Package> packageList = new List<Package>();
            if (Session["cart1"] != null)
            {
                packageList = (List<Package>)Session["cart2"];
            }
            Package existingproduct = packageList.Where(x => x.Package_ID == id).FirstOrDefault();
            if (existingproduct != null)
            {

            }
            else
            {
                Package service = db.Packages.Where(x => x.Package_ID == id).FirstOrDefault();
                packageList.Add(service);
                Session["Cart2"] = packageList;

            }
            return RedirectToAction("Displaybooking");

        }

        public ActionResult PlusToCart2(int id)
        {
            List<Package> tblhallList = new List<Package>();
            if (Session["cart2"] != null)
            {
                tblhallList = (List<Package>)Session["cart2"];
            }
            tblhallList[id].Quantity += 5;
            Session["cart2"] = tblhallList;
            return RedirectToAction("Displaybooking");

        }
        public ActionResult MinusFromCart2(int id)
        {
            List<Package> tblHallList = new List<Package>();
            if (Session["cart2"] != null)
            {
                tblHallList = (List<Package>)Session["cart2"];
            }
            tblHallList[id].Quantity--;
            if (tblHallList[id].Quantity < 0)
            {
                tblHallList.RemoveAt(id);
            }
            Session["cart2"] = tblHallList;

            return RedirectToAction("Displaybooking");
        }
        public ActionResult Removefromcart2(int id)
        {
            List<Package> hallList = new List<Package>();
            if (Session["Cart2"] != null)
            {
                hallList = (List<Package>)Session["Cart2"];
            }

            hallList.RemoveAt(id);
            Session["Cart2"] = hallList;
            return RedirectToAction("Displaybooking");
        }
        public ActionResult PlusToCart(int id)
        {
            List<Hall> tblhallList = new List<Hall>();
            if (Session["cart"] != null)
            {
                tblhallList = (List<Hall>)Session["cart"];
            }
            tblhallList[id].Quantity += 5;
            Session["cart"] = tblhallList;
            return RedirectToAction("Displaybooking");

        }
        public ActionResult MinusFromCart(int id)
        {
            List<Hall> tblHallList = new List<Hall>();
            if (Session["cart"] != null)
            {
                tblHallList = (List<Hall>)Session["cart"];
            }
            tblHallList[id].Quantity--;
            if (tblHallList[id].Quantity < 0)
            {
                tblHallList.RemoveAt(id);
            }
            Session["cart"] = tblHallList;

            return RedirectToAction("Displaybooking");
        }
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
            return RedirectToAction("index", "Home");
        }
    }
}