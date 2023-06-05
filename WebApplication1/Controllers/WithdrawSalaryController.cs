using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace web
    .Controllers
{
    public class WithdrawSalaryController : Controller
    {
        private Model1 db = new Model1();
        public ActionResult SalaryMenu()
        {
            return View();
        }
        public ActionResult Withdraw()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Withdraw(Withdraw_Amount wamount)
        {
            if (wamount.Withdraw_Amount1 <= BaseHelper.totalsalary)
            {
                wamount.Request_Time = System.DateTime.Now;
                wamount.Recive_Time = System.DateTime.Now;
                wamount.Organizer_FID = BaseHelper.event_organizers.EventOrganizer_ID;
                db.Withdraw_Amount.Add(wamount);
                db.SaveChanges();
                TempData["success"] = "Withdraw amount request has been sent to Admin!!";
            }
            else
            {
                TempData["error"] = "You have Insufficent Amount in your wallet !!";
            }
            return RedirectToAction("Withdraw");
        }

        public ActionResult Withdrawhistory()
        {
            var query = db.Withdraw_Amount.Where(x => x.IsTranffered == true && x.Organizer_FID == BaseHelper.event_organizers.EventOrganizer_ID).ToList();
            return View(query);
        }

        public ActionResult Withdrawrequest()
        {
            var query = db.Withdraw_Amount.Where(x => x.IsTranffered != true).ToList();
            return View(query);
        }
        public ActionResult TransferAmount(int id)
        {
            var query = db.Withdraw_Amount.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            Session["query"] = query.Id;
            int dollerrate = (int)MailProvider.GetCurrentDollorprice();
            return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=iqrabajwa00@gmail.com&item_name=EMS&return=https://localhost:44373/WithdrawSalary/PaymentTransferred&amount=" + query.Withdraw_Amount1 / dollerrate);
        }
        public ActionResult PaymentTransferred()
        {
            int id = (int)Session["query"];
            var query = db.Withdraw_Amount.Where(x => x.Id == id).FirstOrDefault();
            query.IsTranffered = true;
            query.Recive_Time = System.DateTime.Now;

            db.Entry(query).State = EntityState.Modified;
            db.SaveChanges();
            TempData["success"] = "Amount has been Transferred successfully";
            return RedirectToAction("AllWithdrawhistory");
        }
        public ActionResult AllWithdrawhistory()
        {
            var query = db.Withdraw_Amount.Where(x => x.IsTranffered == true).ToList();
            return View(query);
        }

    }
}