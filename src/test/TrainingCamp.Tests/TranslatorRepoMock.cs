using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Tests
{

    public class TranslatorRepoMock : ITranslatorRepo
    {
        public WebText GetTranslation(WebText webText, string targetLang)
        {
            var wText = new WebText
            {
                View = webText.View,
                Lang = targetLang,
                Name = webText.Name,
                HtmlText = "Översatt Text " + targetLang + "赤春の花の木" + webText.Name,
                Translator = "Bing"
            };
            return wText;
        }
        public List<WebText> GetTranslation(List<WebText> webTextList, string sourceLang, string targetLang)
        {
             Debug.Assert (webTextList.Count>0);
            return webTextList.Select(wt => new WebText
            {
                View = wt.View,
                Lang = targetLang,
                Name = wt.Name,
                HtmlText = "Översatt Text " + targetLang + "赤春の花の木" + wt.Name,
            }).ToList();
        }
    }
}
