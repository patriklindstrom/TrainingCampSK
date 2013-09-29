using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Http.OData.Routing;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Web.Models
{
    public class WebTextTranslationViewModel
    {
        public WebText SourceLang { get; set; }
        public WebText TargetLang { get; set; }
    }
}