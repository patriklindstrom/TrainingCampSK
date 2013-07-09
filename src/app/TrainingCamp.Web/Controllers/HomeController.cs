using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Controllers
{
    public class HomeController : Controller
    {
        //TODO fix so this is Dependency injected instead
        public IWebTextRepo WebTextRepo = new Repository.WebTextRepo();
        public ActionResult Index(string lang="en")
        {
            List<WebText> webTexts = this.WebTextRepo.GetAllWebTextRepoForView("Home", lang);
            ViewBag.Message = "Shorjini Kempo Camp Stockholm 2014 homepage";

            return View(webTexts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
