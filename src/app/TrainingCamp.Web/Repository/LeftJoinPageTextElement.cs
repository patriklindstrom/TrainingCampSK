using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace TrainingCamp.Web.Repository
{
// ReSharper disable once InconsistentNaming The class name suffix implies for what combination of languages it is used for 
    public class LeftJoinPageTextElement_en_sv : AbstractMultiMapIndexCreationTask<WebTextCombined>
    {
        private const string FROM_LANG = "en";
        private const string TO_LANGUAGE = "sv";

        public LeftJoinPageTextElement_en_sv()
        {
            AddMap<WebText>(baseElements =>
                from baseElement in baseElements.Where(l => l.Lang == FROM_LANG)
                select
                    new WebTextCombined
                    {
                        View = baseElement.View,
                        Name = baseElement.Name,
                        WebTextLeft = new WebText()
                        {
                           
                            //ID
                            Id = baseElement.Id,
                            //Keys
                            View = baseElement.View,
                            Name = baseElement.Name,
                            Lang = baseElement.Lang,
                            //Data
                            HtmlText = baseElement.HtmlText,
                            Comment = baseElement.Comment,
                            //Metadata
                            CreationTime = baseElement.CreationTime,
                            Translator = baseElement.Translator
                        },
                        WebTextRight = (WebText) null
                    });

            AddMap<WebText>(compareElements =>
                from compareElement in compareElements.Where(l => l.Lang == TO_LANGUAGE)
                select
                    new WebTextCombined
                    {
                        //Keys
                        View = compareElement.View,
                        Name = compareElement.Name,
                        //Data
                        WebTextLeft = (WebText) null,
                        WebTextRight = new WebText()
                        {
                            //ID
                            Id = compareElement.Id,
                            //Keys
                            View = compareElement.View,
                            Name = compareElement.Name,
                            Lang = compareElement.Lang,
                            //Data
                            HtmlText = compareElement.HtmlText,
                            Comment = compareElement.Comment,
                            //Metadata
                            CreationTime = compareElement.CreationTime,
                            Translator = compareElement.Translator
                        },
                    });
            Reduce = results => from result in results
                group result by
                    new { result.View, result.Name }
                into g
                select new WebTextCombined ()
                {//Keys
                    View = g.Key.View,
                    Name = g.Key.Name,
                    //data
                    WebTextLeft = new WebText()
                {
                    Id = g.Select(x=>x.WebTextLeft.Id).FirstOrDefault(x => x != null),
                    //Keys
                    View = g.Key.View,
                    Name = g.Key.Name,
                    Lang = g.Select(x => x.WebTextLeft.Lang).FirstOrDefault(x => x != null),
                    //Data
                    HtmlText = g.Select(x => x.WebTextLeft.HtmlText).FirstOrDefault(x => x != null),
                    Comment = g.Select(x => x.WebTextLeft.Comment).FirstOrDefault(x => x != null),
                    //Metadata
                    CreationTime = g.Select(x => x.WebTextLeft.CreationTime).FirstOrDefault(x => x != null),
                    Translator = g.Select(x => x.WebTextLeft.Translator).FirstOrDefault(x => x != null),
                    
                }
                ,

                WebTextRight = new WebText() 
                 {
                     Id = g.Select(x => x.WebTextRight.Id).FirstOrDefault(x => x != null),
                     //Keys
                     View = g.Key.View,
                     Name = g.Key.Name,
                     Lang = g.Select(x => x.WebTextRight.Lang).FirstOrDefault(x => x != null),
                     //Data
                     HtmlText = g.Select(x => x.WebTextRight.HtmlText).FirstOrDefault(x => x != null),
                     Comment = g.Select(x => x.WebTextRight.Comment).FirstOrDefault(x => x != null),
                     //Metadata
                     CreationTime = g.Select(x => x.WebTextRight.CreationTime).FirstOrDefault(x => x != null),
                     Translator = g.Select(x => x.WebTextRight.Translator).FirstOrDefault(x => x != null),

                 }
                };
            Index(x => x.WebTextRight.HtmlText, FieldIndexing.Analyzed);
        }
    }
}