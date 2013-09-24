using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;

namespace TrainingCamp.Web.Repository
{

    public class LeftJoinPageTextElement : AbstractMultiMapIndexCreationTask<WebTextCombined>
    {
        public LeftJoinPageTextElement()
        {
            AddMap<WebTextCombined>(baseElements =>
                                    from baseElement in baseElements.Where(l => l.Language == "en")
                                    select
                                        new
                                        {
                                            baseElement.Page,
                                            baseElement.Token,
                                            baseElement.Webtext,
                                            WebtextCompare = (string)null,
                                            Count = 0
                                        });

            AddMap<WebTextCombined>(compareElements =>
                                    from compareElement in compareElements.Where(l => l.Language == "sv")
                                    select
                                        new
                                        {
                                            compareElement.Page,
                                            compareElement.Token,
                                            Webtext = (string)null,
                                            WebtextCompare = compareElement.Webtext,
                                            Count = 1
                                        }
                );
            Reduce = results => from result in results
                                group result by
                                    new { result.Page, result.Token }
                                    into g
                                    select new
                                    {
                                        g.Key.Page,
                                        g.Key.Token,
                                        Webtext = g.Select(x => x.Webtext).FirstOrDefault(x => x != null),
                                        WebtextCompare = g.Select(x => x.WebtextCompare).FirstOrDefault(x => x != null),
                                        Count = g.Sum(x => x.Count)
                                    };
            Index(x => x.Webtext, FieldIndexing.Analyzed);
        }
    }
}