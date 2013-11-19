using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingCamp.Web.Models;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Controllers
{
    public class HomeController : Controller
    {
        //TODO fix so this is Dependency injected instead
        public IWebTextRepo WebTextRepo = new Repository.WebTextRepoRavenDB();
        
      // [OutputCache(Duration = 600, VaryByParam = "lang")]
        public ActionResult Index(string lang="en")
        {

            WebTextViewBag webTextViewBag = null;
            List<WebText> webTexts = this.WebTextRepo.SearchWebText("Home", lang);
            MissingWebTextHandler missingWebTextHandler = new Repository.MissingWebTextFixer("Home", WebTextRepo);
           
            if (webTexts!=null)
            {
                 webTextViewBag = new WebTextViewBag(webTexts,missingWebTextHandler); 
            }
            else
            {
                throw new NullReferenceException("Could not get webtext from WebTextRepo.SearchWebText");
            }
            
            ViewBag.Message = "Shorjini Kempo Camp Stockholm 2014 homepage";

            return View(webTextViewBag);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult ClearCache()
        {
            //    defaults: new { controller = "Home", action = "Index", lang = UrlParameter.Optional }
            var path = Url.Action("Index", "Home",new{controller="Home",action = "Index" ,lang="en"});
            var defaultProvider = "";
            HttpResponse.RemoveOutputCacheItem(path);
            return RedirectToAction("Index");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
