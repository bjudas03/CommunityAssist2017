using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class LoginController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName, Password")]LoginClass lc)
        {
            int results = db.usp_Login(lc.UserName, lc.Password);
            int perKey = 0;
            Message msg = new Message();
            if (results != -1)
            {
                var pkey = (from p in db.People
                            where p.PersonEmail.Equals(lc.UserName)
                            select p.PersonKey).FirstOrDefault();
                perKey = (int)pkey;
                Session["PersonKey"] = perKey;

                msg.MessageText = "Welcome, " + lc.UserName;
            }
            else
            {
                msg.MessageText = "Invalid Login";
            }
            return View("Result", msg);
        }
           
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }


    }
}