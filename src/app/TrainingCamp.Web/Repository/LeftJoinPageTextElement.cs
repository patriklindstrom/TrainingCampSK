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
    public class LeftJoinPageTextElementEnSv : AbstractMultiMapIndexCreationTask<WebTextCombinedLight>
    {
        private const string FROM_LANG = "en";
        private const string TO_LANGUAGE = "sv";

        public LeftJoinPageTextElementEnSv()
        {
            AddMap<WebText>(baseElements =>
                from baseElement in baseElements.Where(l => l.Lang == FROM_LANG)
                select
                    new WebTextCombinedLight
                    {
                        View = baseElement.View,
                        Name = baseElement.Name,
                        WebLeftId = baseElement.Id,
                        WebLeftHtmlText  = baseElement.HtmlText,
                        WebLeftComment = baseElement.Comment,
                        WebLeftCreationTime = baseElement.CreationTime,
                        WebLeftTranslator = baseElement.Translator,
                        WebRightId = (string)null,
                        WebRightHtmlText  = (string)null,
                        WebRightComment =  (string)null,
                        WebRightCreationTime = (DateTime?)null,
                        WebRightTranslator = (string)null,
                        Count = 0
                    });

            AddMap<WebText>(compareElements =>
                from compareElement in compareElements.Where(l => l.Lang == TO_LANGUAGE)
                select
                    new WebTextCombinedLight
                    {
                        View = compareElement.View,
                        Name = compareElement.Name,
                        WebLeftId = (string)null,
                        WebLeftHtmlText = (string)null,
                        WebLeftComment = (string)null,
                        WebLeftCreationTime = (DateTime?)null,
                        WebLeftTranslator = (string)null,
                        WebRightId = compareElement.Id,
                        WebRightHtmlText = compareElement.HtmlText,
                        WebRightComment = compareElement.Comment,
                        WebRightCreationTime = compareElement.CreationTime,
                        WebRightTranslator = compareElement.Translator,
                        Count = 1
                    });
            Reduce = results => from result in results
                group result by
                    new { result.View, result.Name }
                into g
                select new WebTextCombinedLight ()
                {//Keys
                    View = g.Key.View,
                    Name = g.Key.Name,
                        WebLeftId =  g.Select(x => x.WebLeftId).FirstOrDefault(x => x != null),
                        WebLeftHtmlText =  g.Select(x => x.WebLeftHtmlText).FirstOrDefault(x => x != null),
                        WebLeftComment = g.Select(x => x.WebLeftComment).FirstOrDefault(x => x != null),
                        WebLeftCreationTime = g.Select(x => x.WebLeftCreationTime).FirstOrDefault(x => x != null),
                        WebLeftTranslator = g.Select(x => x.WebLeftTranslator).FirstOrDefault(x => x != null),
                        WebRightId = g.Select(x => x.WebRightId).FirstOrDefault(x => x != null),
                        WebRightHtmlText = g.Select(x => x.WebRightHtmlText).FirstOrDefault(x => x != null),
                        WebRightComment =  g.Select(x => x.WebRightComment).FirstOrDefault(x => x != null),
                        WebRightCreationTime =  g.Select(x => x.WebRightCreationTime).FirstOrDefault(x => x != null),
                        WebRightTranslator = g.Select(x => x.WebRightTranslator).FirstOrDefault(x => x != null),         
                        Count = g.Sum(x=>x.Count)
                };
            Index(x => x.View, FieldIndexing.Analyzed);
        }

    }
    public class LeftJoinPageTextElementEnJp : AbstractMultiMapIndexCreationTask<WebTextCombinedLight>
    {
        private const string FROM_LANG = "en";
        private const string TO_LANGUAGE = "ja";

        public LeftJoinPageTextElementEnJp()
        {
            AddMap<WebText>(baseElements =>
                from baseElement in baseElements.Where(l => l.Lang == FROM_LANG)
                select
                    new WebTextCombinedLight
                    {
                        View = baseElement.View,
                        Name = baseElement.Name,
                        WebLeftId = baseElement.Id,
                        WebLeftHtmlText = baseElement.HtmlText,
                        WebLeftComment = baseElement.Comment,
                        WebLeftCreationTime = baseElement.CreationTime,
                        WebLeftTranslator = baseElement.Translator,
                        WebRightId = (string)null,
                        WebRightHtmlText = (string)null,
                        WebRightComment = (string)null,
                        WebRightCreationTime = (DateTime?)null,
                        WebRightTranslator = (string)null,
                        Count = 0
                    });

            AddMap<WebText>(compareElements =>
                from compareElement in compareElements.Where(l => l.Lang == TO_LANGUAGE)
                select
                    new WebTextCombinedLight
                    {
                        View = compareElement.View,
                        Name = compareElement.Name,
                        WebLeftId = (string)null,
                        WebLeftHtmlText = (string)null,
                        WebLeftComment = (string)null,
                        WebLeftCreationTime = (DateTime?)null,
                        WebLeftTranslator = (string)null,
                        WebRightId = compareElement.Id,
                        WebRightHtmlText = compareElement.HtmlText,
                        WebRightComment = compareElement.Comment,
                        WebRightCreationTime = compareElement.CreationTime,
                        WebRightTranslator = compareElement.Translator,
                        Count = 1
                    });
            Reduce = results => from result in results
                                group result by
                                    new { result.View, result.Name }
                                    into g
                                    select new WebTextCombinedLight()
                                    {//Keys
                                        View = g.Key.View,
                                        Name = g.Key.Name,
                                        WebLeftId = g.Select(x => x.WebLeftId).FirstOrDefault(x => x != null),
                                        WebLeftHtmlText = g.Select(x => x.WebLeftHtmlText).FirstOrDefault(x => x != null),
                                        WebLeftComment = g.Select(x => x.WebLeftComment).FirstOrDefault(x => x != null),
                                        WebLeftCreationTime = g.Select(x => x.WebLeftCreationTime).FirstOrDefault(x => x != null),
                                        WebLeftTranslator = g.Select(x => x.WebLeftTranslator).FirstOrDefault(x => x != null),
                                        WebRightId = g.Select(x => x.WebRightId).FirstOrDefault(x => x != null),
                                        WebRightHtmlText = g.Select(x => x.WebRightHtmlText).FirstOrDefault(x => x != null),
                                        WebRightComment = g.Select(x => x.WebRightComment).FirstOrDefault(x => x != null),
                                        WebRightCreationTime = g.Select(x => x.WebRightCreationTime).FirstOrDefault(x => x != null),
                                        WebRightTranslator = g.Select(x => x.WebRightTranslator).FirstOrDefault(x => x != null),
                                        Count = g.Sum(x => x.Count)
                                    };
            Index(x => x.View, FieldIndexing.Analyzed);
        }

    }
}