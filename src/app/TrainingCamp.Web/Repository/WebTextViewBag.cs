using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingCamp.Web.Repository
{
    public class WebTextViewBag
    {
        public WebTextViewBag(List<WebText> webTexts)
        {
            WebTexts = webTexts;
        }

        private List<WebText> WebTexts { get; set; }

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
            var webtText = WebTexts.FirstOrDefault(t => t.Name == name);
            if (webtText != null)
            {
                var htmlText = webtText.HtmlText;
            
                string returnText = string.IsNullOrEmpty(htmlText) ? defaultText : htmlText;

                return returnText;
            }
            else
            {
                throw new NullReferenceException("The List of webText is null cant look up any html texts");
            }
        }
    }
}