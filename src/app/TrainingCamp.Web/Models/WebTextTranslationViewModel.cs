using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Models
{
    public class CombinedTranslationText
    {
        //ID
        public String Id { get; set; }
        //Keys
        public String Name { get; set; }
        //Data 1
        public String HtmlTextShortLeft { get; set; }
        public String CommentShortLeft { get; set; }
        //Metadata 1
        public string CreationTimeLeft { get; set; }
        public string TranslatorLeft { get; set; }
        //Data 1
        public String HtmlTextShortRight { get; set; }
        public String CommentShortRight { get; set; }
        //Metadata 1
        public string CreationTimeRight { get; set; }
        public string TranslatorRight { get; set; }
    }

    public class WebTextTranslationViewModel
    {
        private const int DISPLAY_LENGTH = 10;
        private const string DISPLAY_NULL = "N/A";
        private string DisplayShort(string longtext)
        {
            string returnText = longtext ?? DISPLAY_NULL;//string.Empty;
            if (returnText.Length > DISPLAY_LENGTH)
            {
                returnText = returnText.Substring(0, DISPLAY_LENGTH) + "...";
            }
            return returnText;
        }
        private string DisplayPossibleNullSide(string weakText)
        {
            string returnText = weakText ?? "N/A";//string.Empty;

            return returnText;
        }

        public WebTextTranslationViewModel(List<WebTextCombined> combos)
        {
            CombinedTranslationTexts = new List<CombinedTranslationText>();
            foreach (var combo in combos)
            {
                CombinedTranslationTexts.Add(new CombinedTranslationText
                    {
                        Id = combo.WebTextLeft.Id,
                        Name = combo.WebTextLeft.Name,
                        HtmlTextShortLeft = DisplayShort(combo.WebTextLeft.HtmlText),
                        CommentShortLeft = DisplayShort(combo.WebTextLeft.Comment),
                        CreationTimeLeft = combo.WebTextLeft.CreationTime.ToShortDateString(),
                        TranslatorLeft = combo.WebTextLeft.Translator,
                        //Check if the right object in the left join exist
                        //HtmlTextShortRight = {return combo.WebTextRight != null ? DisplayShort(combo.WebTextRight.HtmlText): DISPLAY_NULL},
                        //CommentShortRight = {return combo.WebTextRight != null ? DisplayShort(combo.WebTextRight.Comment) : DISPLAY_NULL},
                        //CreationTimeRight = {return combo.WebTextRight != null ? combo.WebTextRight.CreationTime.ToShortDateString() : DISPLAY_NULL},
                        //TranslatorRight = {return combo.WebTextRight != null ? combo.WebTextRight.Translator : DISPLAY_NULL}
                    });
            }
        }

        public List<CombinedTranslationText> CombinedTranslationTexts { get; set; }
    }
}