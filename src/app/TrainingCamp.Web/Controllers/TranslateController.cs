using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Controllers
{
    public class TranslateController : Controller
    {

        public IWebTextRepo WebTextRepo = new Repository.WebTextRepoRavenDB();
        //
        // GET: /Translate/

        public ActionResult Index(string controllername, string actionname, string langname)
        {
            ViewBag.Message = "Translate Shorjini Kempo Camp Stockholm 2014 for language " + langname;


            return View();
        }

    }
}
