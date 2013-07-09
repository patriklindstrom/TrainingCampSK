using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingCamp.Web.Repository
{
    public class WebTextViewBag
    {
        public WebTextViewBag(IEnumerable<WebText> webTexts)
        {
            WebTextDictionary = webTexts.ToDictionary(k=>k.Name,t=>t.HtmlText);
        }

      //  private List<WebText> WebTexts { get; set; }
        private Dictionary<string,string> WebTextDictionary { get; set; }

        public string GetWebHtmlText(string name, string defaultText)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name","There has to be a key to look up the translated html text in the view");
            }
            if (String.IsNullOrEmpty(defaultText))
            {
                throw new ArgumentNullException("defaultText", "There has to be a defaultText in the view as a backup");
            }

            string htmlText = null;
            string webtText = null;
            if (WebTextDictionary != null)
            {
                if (WebTextDictionary.TryGetValue(name, out webtText))
                {
                    htmlText = webtText;
                }
                string returnText = string.IsNullOrEmpty(htmlText) ? defaultText : htmlText;
                return returnText;
            }
            else
            {
                throw new NullReferenceException("The  WebTextDictionary is null cant look up any html texts");
            }
        }

        public MvcHtmlString GetWebHtmlTextWithQuotes(string name, string defaultText)
        {
            //Todo check for XSS ? Html encode ? Or should that just be done when entering data.
            var tempName = (GetWebHtmlText(name, defaultText)); ; 
            var   tempName2 = "\"" + tempName + "\"";
            return MvcHtmlString.Create(tempName2);
        }
    }
}