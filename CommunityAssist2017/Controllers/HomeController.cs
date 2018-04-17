using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017.Models; //References the project Models

namespace CommunityAssist2017.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities(); // Initialize the Entities Classes
            return View(db.GrantTypes.ToList()); //Pass the collection GrantTypes to the index as a list
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}