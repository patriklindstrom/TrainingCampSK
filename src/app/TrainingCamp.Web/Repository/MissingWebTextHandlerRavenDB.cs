using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingCamp.Web.Repository
{
    public abstract class MissingWebTextHandler
    {
        protected string ViewKey;

        public MissingWebTextHandler(string viewKey)
        {
            ViewKey = viewKey;
        }

        public abstract void DealWithIt(string name, string defaultText);
    }

    /// <summary>
    /// All text that are not in the database this class
    /// puts the default value in the database.
    /// </summary>
    public class MissingWebTextFixer : MissingWebTextHandler
    {
        private const string DEFAULT_LANG = "en";
        private readonly IWebTextRepo _webTextRepo;

        public MissingWebTextFixer(string viewKey, IWebTextRepo webTextRepo) : base(viewKey)
        {
            _webTextRepo = webTextRepo;
        }


        public override void DealWithIt(string name, string defaultText)
        {
            //TODO: move this to repository and if exist make an update instead. Change to Upsert?
            //Check if it exist. It is possible that two keys are on same page and they are
            if (_webTextRepo.WebTextExist(name: name, viewName: ViewKey, lang: DEFAULT_LANG)) return;
            var missingWebText = new WebText(translator: "MissingWebTextFixer")
                {
                    View = ViewKey,
                    Name = name,
                    HtmlText = defaultText,
                    Lang = DEFAULT_LANG
                };
            _webTextRepo.AddWebText(missingWebText);
        }
    }
}