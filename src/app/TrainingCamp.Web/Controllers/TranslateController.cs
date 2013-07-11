using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingCamp.Web.Models;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Controllers
{
    public class TranslateController : Controller
    {

        public IWebTextRepo WebTextRepo = new Repository.WebTextRepoRavenDB();
        //
        // GET: /Translate/

        public ActionResult Index(string controllername, string actionname, string langname,string fromlang="en")
        {
            ViewBag.Message = "Translate Shorjini Kempo Camp Stockholm 2014 for language " + langname;
            WebTextTranslationViewModel webTextTranslationviewModel = null;
            List<WebText> webTexts = this.WebTextRepo.SearchWebText(viewName: controllername,lang: langname);


            if (webTexts != null)
            {
                webTextTranslationviewModel = new WebTextTranslationViewModel(webTexts);
            }
            else
            {
                throw new NullReferenceException("Could not get webtext from WebTextRepo.SearchWebText for translation");
            }

            ViewBag.Message = "Shorjini Kempo Camp Stockholm 2014 homepage";

            return View(webTextTranslationviewModel);

            return View();
        }

    }

 
}
