﻿using System;
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
        public TranslatorRepo TranslateRepo = new TranslatorRepo();
        //
        // GET: /Translate/
        public TranslateController()
        {
        }
        public ActionResult Index(string controllername, string actionname, string langname,string fromlang="en")
        {
            if (controllername == null) throw new ArgumentNullException("controllername");
            ViewBag.Message = "Translate Shorjini Kempo Camp Stockholm 2014 for language " + langname;
            WebTextTranslationListViewModel webTextTranslationListviewModel = null;
            List<WebTextCombinedLight> webTextCombinedLight = this.WebTextRepo.SearchWebTextLeftJoin(viewName: controllername, rightLang: fromlang, leftLang: langname);
            if (webTextCombinedLight != null)
            {
                webTextTranslationListviewModel = new WebTextTranslationListViewModel(webTextCombinedLight, langname,controllername)
                {
                    SourceLang = fromlang
                };
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
                // http://localhost:52332/home/index/sv/translate/en
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
            var translateRepo = new TranslatorRepo();
            string webTranslateToken = translateRepo.GetBingToken();
            if (webTextSource != null && webTextTarget != null)
            {
                webTextTranslationviewModel = new WebTextTranslationViewModel
                {
                    SourceLang = webTextSource,
                    TargetLang = webTextTarget,
                    AccessToken = webTranslateToken
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
        public ActionResult CreateTextItem(string view, string sourceLang)
        {
            if (view == null) throw new ArgumentNullException("view");
            
            if (sourceLang == null) throw new ArgumentNullException("sourceLang");
            ViewBag.Message = "Create  Webtext for Shorjini Kempo Camp Stockholm 2014 for  " + view;
           
            WebTextTranslationViewModel webTextTranslationviewModel = new WebTextTranslationViewModel
                {
                    SourceLang = new WebText(){View = view,Lang = sourceLang},
                    TargetLang =null
                };
       
            return View(webTextTranslationviewModel);
        }
        [HttpPost]
        public ActionResult CreateTextItem(string view, string sourceLang, string targetLang, FormCollection values)
        {
            WebText tLWebText = new WebText();
            try
            {
                // TODO: Add insert logic here
                tLWebText.HtmlText = values["SourceLang.HtmlText"];
                tLWebText.View = view;
                tLWebText.Name = values["SourceLang.Name"];
                tLWebText.Lang = sourceLang;
                tLWebText.Translator = values["SourceLang.Translator"];
                tLWebText.Comment = values["SourceLang.Comment"];                         
                tLWebText.View = view;
                this.WebTextRepo.StoreWebText(tLWebText);
                // http://localhost:52332/home/index/sv/translate/en
                // url: "{controllername}/{actionname}/{langname}/translate/{fromlang}",
                return RedirectToAction("Index", new { controllername = view, actionname = "index", langname = targetLang, fromLang = "en" });
            }
            catch
            {
                return View();
            }

        }

        public ActionResult TranslateAll(string sourceLang, string targetLang,string view)
        {
            try
            {
                var translatedWebTexts = TranslateWebTexts(sourceLang, targetLang);
                this.WebTextRepo.StoreWebTexts(translatedWebTexts);
                return RedirectToAction("Index", new { controllername = view, actionname = "index", langname = targetLang, fromLang = "en" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { controllername = view, actionname = "index", langname = targetLang, fromLang = "en" });
                //Console.WriteLine(e);
            }

        }

        public List<WebText> TranslateWebTexts(string sourceLang, string targetLang)
        {
            List<WebText> webTexts = this.WebTextRepo.ListWebTextForLang(language: sourceLang);
            List<WebText> translatedWebTexts = this.TranslateRepo.TranslateAll(sourceLang: sourceLang, targetLang:targetLang,webTexts:webTexts);
            return translatedWebTexts;
        }
    }


}
