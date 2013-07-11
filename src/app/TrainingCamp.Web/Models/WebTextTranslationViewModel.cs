using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Models
{
    public class WebTextTranslationViewModel
    {
        public WebTextTranslationViewModel(List<WebText> webTexts)
        {
            
        }
        public List<WebText> WebTexts { get; set; }

    }
}