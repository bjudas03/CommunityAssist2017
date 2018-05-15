using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class GrantApplicationsController : Controller
    {
        // GET: GrantApplications
        public ActionResult Index()
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            if (Session["PersonKey"] == null)
            {
                //Message m = new Message("You must be logged in to donate!");
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Result", m);
            }
            ViewBag.GrantList = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include = "PersonKey," +
            "GrantApplicationDate, " +
            "GrantTypeKey," +
            "GrantApplicationRequestAmount," +
            "GrantApplicationReason," +
            "GrantApplicationStatusKey," +
            "GrantApplicationAllocationAmount")] GrantApplication ga)
        {
            try
            {
                CommunityAssist2017Entities db = new CommunityAssist2017Entities();
                ga.PersonKey = (int)Session["PersonKey"];
                ga.GrantAppicationDate = DateTime.Now;
                ga.GrantApplicationStatusKey = (int)1;
                //ga.GrantTypeKey = 
  

               // CommunityAssist2017Entities db = new CommunityAssist2017Entities();
                db.GrantApplications.Add(ga);
                db.SaveChanges();
                Message m = new Message();
                m.MessageText = "Your application is being reviewed";
                return RedirectToAction("Result", m);
            }
            catch(Exception e)
            {
                Message m = new Message();
                m.MessageText = e.Message;
                return RedirectToAction("Result", m);
            }
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }

        public ActionResult Details(List<GrantApplication> grants)
        {
            return View(grants);
        }
    }
}