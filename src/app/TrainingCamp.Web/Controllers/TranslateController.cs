using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public TranslateController()
        {
        }

        public ActionResult Index(string controllername, string actionname, string langname,string fromlang="en")
        {
            if (controllername == null) throw new ArgumentNullException("controllername");
            ViewBag.Message = "Translate Shorjini Kempo Camp Stockholm 2014 for language " ;
            WebTextTranslationListViewModel webTextTranslationListviewModel = null;

            List<WebTextCombinedLight> webTextCombinedLight = this.WebTextRepo.SearchWebTextLeftJoin(viewName: controllername, rightLang: fromlang, leftLang: langname);


            if (webTextCombinedLight != null)
            {
                webTextTranslationListviewModel = new WebTextTranslationListViewModel(webTextCombinedLight, langname,controllername);
            }
            else
            {
                throw new NullReferenceException("Could not get webtext from WebTextRepo.SearchWebText for translation");
            }



            return View(webTextTranslationListviewModel);

            
        }
        
        public ActionResult Details(string SourceLangId, string TargetLangId)
        {
            
            return View();
        }

        public ActionResult Create(string sourceLangId,string targetLang)
        {
            if (sourceLangId == null) throw new ArgumentNullException("sourceLangId");
            if (targetLang == null) throw new ArgumentNullException("targetLang");
            ViewBag.Message = "Create Translation Webtext for Shorjini Kempo Camp Stockholm 2014 for language " + targetLang;
            WebTextTranslationViewModel webTextTranslationviewModel = null;

            WebText webTextSource = this.WebTextRepo.GetWebTextRepo(sourceLangId);
            

            if (webTextSource != null )
            {
                webTextTranslationviewModel = new WebTextTranslationViewModel
                {
                    SourceLang = webTextSource,
                    TargetLang = new WebText { Lang = targetLang }
                };
            }
            else
            {
                throw new NullReferenceException("Could not get source language webtext from WebTextRepo. Create for translation");
            }
            return View(webTextTranslationviewModel);
           
        }

        [HttpPost]
        public ActionResult Create( string view,string name,string targetLang,FormCollection values)
        {
            WebText tLWebText = new WebText();
            try
            {
                // TODO: Add insert logic here
                tLWebText.HtmlText = values["TargetLang.HtmlText"];
                tLWebText.Translator = values["TargetLang.Translator"];
                tLWebText.Comment = values["TargetLang.Comment"];
                tLWebText.Lang = targetLang;
                tLWebText.Name = name;
                tLWebText.View = view;
                this.WebTextRepo.StoreWebText(tLWebText);
                // http://localhost:52332/Home/index/sv/translate/en
                // url: "{controllername}/{actionname}/{langname}/translate/{fromlang}",
                return RedirectToAction("Index", new { controllername = view, actionname="index",langname=targetLang,fromLang="en" });
            }
            catch
            {
                return View();
            }

        }
        // GET: /Fundlist/Edit/5

        public ActionResult Edit(string SourceLangId, string TargetLangId)
        {


            if (SourceLangId == null) throw new ArgumentNullException("SourceLangId");
            if (TargetLangId == null) throw new ArgumentNullException("TargetLangId");
            ViewBag.Message = "Translate Shorjini Kempo Camp Stockholm 2014 for language " + SourceLangId;
            WebTextTranslationViewModel webTextTranslationviewModel = null;

            WebText webTextSource = this.WebTextRepo.GetWebTextRepo(SourceLangId);
            WebText webTextTarget = this.WebTextRepo.GetWebTextRepo(TargetLangId);

            if (webTextSource != null && webTextTarget != null)
            {
                webTextTranslationviewModel = new WebTextTranslationViewModel
                {
                    SourceLang = webTextSource,
                    TargetLang = webTextTarget
                };
            }
            else
            {
                throw new NullReferenceException("Could not get source or target language webtext from WebTextRepo.SearchWebText for translation");
            }
            return View(webTextTranslationviewModel);
        }

        //
        // POST: /Fundlist/Edit/5

        [HttpPost]
        public ActionResult Edit(string view, string name, string targetLang, string targetLangId, FormCollection values)
        {
            WebText tLWebText = new WebText();
            try
            {
                tLWebText.HtmlText = values["TargetLang.HtmlText"];
                tLWebText.Translator = values["TargetLang.Translator"];
                tLWebText.Comment = values["TargetLang.Comment"];
                tLWebText.Lang = targetLang;
                tLWebText.View = view;
                tLWebText.Name = name;
                tLWebText.Id = targetLangId;

                this.WebTextRepo.UpdateWebText(tLWebText);

                return RedirectToAction("Index", new { controllername = view, actionname = "index", langname = targetLang, fromLang = "en" });
            }
            catch
            {
                return View();
            }
        }

    }


}
