using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class DonationController : Controller
    {
        // GET: Donation
        public ActionResult Index()
        {
            if (Session["PersonKey"] == null)
            {
               // Message m = new Message("You must be logged in to donate!");
                return RedirectToAction("Index" , "Login");
               // return RedirectToAction("Result", m);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include ="PersonKey, DonationAmount, DonationDate, DonationConfirmationCode")]Donation d)
        {
            d.DonationConfirmationCode = Guid.NewGuid();
            d.DonationDate = DateTime.Now;
            d.PersonKey = (int)Session["PersonKey"];
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            db.Donations.Add(d);
            db.SaveChanges();
            Message m = new Message("New donation has been entered");
            return View("Details", db.Donations.ToList());
            //return View("Result", m);
        }

        public ActionResult Result(Message msg)
        {
            return View(msg);
        }

        public ActionResult Details(List<Donation> donations)
        {
            return View(donations);
        }
    }
}