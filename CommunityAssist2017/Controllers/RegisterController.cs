using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models;

namespace CommunityAssist2017.Controllers
{
    public class RegisterController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include ="LastName," +
            "FirstName," +
            "Email," +
            "PlainPassword," +
            "Apartment," +
            "Street," +
            "City," +
            "State," +
            "Zipcode," +
            "Phone")]NewPerson np)
        {
            Message msg = new Message();
            int result = db.usp_Register(np.LastName, np.FirstName, np.Email, np.PlainPassword, np.Apartment,
                np.Street, np.City, np.State, np.Zipcode, np.Phone);
            if (result != -1)
            {
                msg.MessageText = "Welcome! Congrats on Registering, " + np.FirstName + " " + np.LastName;
                //return RedirectToAction("Login","Index");

            } 
            else
            {
                msg.MessageText = "Something went wrong! You need to try again. Do better! Be better!";
            }

            return View("Result", msg);
        }



        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
            
    }
}