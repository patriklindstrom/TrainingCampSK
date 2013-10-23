using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Models
{
    public class CombinedTranslationText
    {
        //ID
        public String LeftId { get; set; }
        public String RightId { get; set; }
        //Keys
        public String View { get; set; }
        public String Name { get; set; }
        //Data 1
        public String SourceLangTxt { get; set; }
        public String CommentSource { get; set; }
        //Metadata 1
        public string CreationTimeLeft { get; set; }
        public string SourceWriter { get; set; }
        //Data 2
        public String TargetLangTxt { get; set; }
        public String CommentTarget { get; set; }
        //Metadata 2
        public string CreationTimeRight { get; set; }
        public string TranslatorTarget { get; set; }
    }

    public class WebTextTranslationListViewModel
    {
        public object View { get; set; }
        public string SourceLang { get; set; }
        public string TargetLang { get; set; }
        private const int DISPLAY_LENGTH = 10;
        private const string DISPLAY_NULL = "N/A";

        private string DisplayShort(string longtext)
        {
            string returnText = longtext ?? DISPLAY_NULL; //string.Empty;
            if (returnText.Length > DISPLAY_LENGTH)
            {
                returnText = returnText.Substring(0, DISPLAY_LENGTH) + "...";
            }
            return returnText;
        }

        private string DisplayPossibleNullSide(string weakText)
        {
            string returnText = weakText ?? DISPLAY_NULL; //string.Empty;

            return returnText;
        }

        public WebTextTranslationListViewModel(List<WebTextCombinedLight> combos, string targetLang, string view)
        {
            TargetLang = targetLang;
            View = view;

            CombinedTranslationTexts = new List<CombinedTranslationText>();
            foreach (var combo in combos)
            {
                CombinedTranslationTexts.Add(item: new CombinedTranslationText
                {
                    LeftId = combo.WebLeftId,
                    RightId = combo.WebRightId,
                    View = combo.View,
                    Name = combo.Name,

                    SourceLangTxt = DisplayShort(combo.WebLeftHtmlText),
                    CommentSource = DisplayShort(combo.WebLeftComment),
                    //  CreationTimeLeft = combo.WebLeftCreationTime ?? null ,
                    SourceWriter = combo.WebLeftTranslator,
                    //Check if the right object in the left join exist
                    TargetLangTxt = DisplayShort(combo.WebRightHtmlText),
                    CommentTarget = DisplayShort(combo.WebRightComment),
                    //CreationTimeRight = {return combo.WebTextRight != null ? combo.WebTextRight.CreationTime.ToShortDateString() : DISPLAY_NULL},
                    TranslatorTarget = DisplayShort(combo.WebRightTranslator),
                }
                    );
            }
        }

        public List<CombinedTranslationText> CombinedTranslationTexts { get; set; }

        
    }
}
      