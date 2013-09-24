using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Indexing;

namespace TrainingCamp.Web.Repository
{
    public interface IWebTextRepo
    {
        string GetWebTextRepo(int id);
        List<WebText> SearchWebText(string name);
        List<WebText> SearchWebText(string viewName, string lang);
        List<WebText> SearchWebText(string viewName, string lang,string name);
        Boolean WebTextExist(string viewName, string lang, string name);
        void AddWebText(WebText webText);
        void EditWebText(int webTextId);
        List<WebTextCombined> SearchWebTextLeftJoin(string viewName, string rightLang, string leftLang);
        List<WebTextCombined> SearchWebTextLeftJoinUgly(string viewName, string rightLang, string leftLang);
    }

    public class WebTextRepoRavenDB : IWebTextRepo
    {
        public IDocumentStore RavenDocumentStore
        {
            get { return DocumentStoreHolder.DocumentStore; }
        }

        private IDocumentSession _documentSession;

        private IDocumentSession RavenSession
        {
            get
            {
                if (_documentSession == null)
                {
                    this._documentSession = RavenDocumentStore.OpenSession();
                }
                Debug.Assert(_documentSession != null, "_documentSession != null");
                return this._documentSession;
            }
        }


        public string GetWebTextRepo(int id)
        {
            throw new NotImplementedException();
        }

        public List<WebText> SearchWebText(string name)
        {
            List<WebText> viewSearchReturn = null;
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                viewSearchReturn = RavenSession.Query<WebText>()
                                               .Where(t => t.Name == name)
                                               .ToList();
            }
            return viewSearchReturn;
        }


        public List<WebText> SearchWebText(string viewName, string lang)
        {
            List<WebText> viewSearchReturn = null;
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                viewSearchReturn = RavenSession.Query<WebText>()
                                               .Where(t => t.View == viewName && t.Lang == lang)
                                               .ToList();
            }
            return viewSearchReturn;
        }

        public List<WebText> SearchWebText(string viewName, string lang, string name)
        {
            List<WebText> viewSearchReturn = null;
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                viewSearchReturn = RavenSession.Query<WebText>()
                                               .Where(t => t.View == viewName && t.Lang == lang)
                                               .Where(t => t.Name == name)
                                               .ToList();
            }
            return viewSearchReturn;
        }

        public Boolean WebTextExist(string viewName, string lang, string name)
        {
            return SearchWebText(viewName, lang, name).Any();
        }

        public void AddWebText(WebText webText)
        {
            RavenSession.Store(webText);
            RavenSession.SaveChanges();
        }
        public void AddWebTextList(List<WebText> webTexts)
        {
            RavenSession.Store(webTexts);
            RavenSession.SaveChanges();
        }
        public void EditWebText(int webTextId)
        {
            throw new NotImplementedException();
        }
        public List<WebTextCombined> SearchWebTextLeftJoinUgly (string viewName, string rightLang, string leftLang)
        {

          
            List<WebTextCombined> viewSearchReturn = null;
            using (RavenSession)
            {
                var wTFromLang =
                    from webTextFromLang in
                        RavenSession.Load<List<WebText>>("WebTexts")                     
                        ////<WebText>().Where(f => f.Lang == rightLang && f.View == viewName)
                    select webTextFromLang
                        ;
                List<WebText> wTFromLangList = wTFromLang.ToList();

                var webTextCombinedList = new List<WebTextCombined>();
                
                viewSearchReturn = webTextCombinedList.ToList();
            }
            return viewSearchReturn;
        }
        public List<WebTextCombined> SearchWebTextLeftJoin(string viewName, string rightLang, string leftLang)
        {

            /*
             
var query = (from p in dc.GetTable<Person>()
join pa in dc.GetTable<PersonAddress>() on p.Id equals pa.PersonId into tempAddresses
from addresses in tempAddresses.DefaultIfEmpty()
select new { p.FirstName, p.LastName, addresses.State });


SQL Translation

-- Context: SqlProvider(Sql2005) Model: AttributedMetaModel Build: 3.5.21022.8
SELECT [t0].[FirstName], [t0].[LastName], [t1].[State] AS [State]
FROM [dbo].[Person] AS [t0]
LEFT OUTER JOIN [dbo].[PersonAddress] AS [t1] ON [t0].[Id] = [t1].[PersonID]
             * 
             * var orderForBooks = from bk in bookList
            join ordr in bookOrders
            on bk.BookID equals ordr.BookID
            into a
            from b in a.DefaultIfEmpty(new Order())
            select new
            {
                bk.BookID,
                Name = bk.BookNm,
                b.PaymentMode
            };
             * 
             *  var webTextList = RavenSession.Query<WebText>()
                                               .Where(t => t.View == viewName && t.Lang == rightLang)                                             
                                               .ToList();
             * 
             * public String Id { get; set; }
        //Keys
        public String View { get; set; }
        public String Name { get; set; }
        public String Lang { get; set; }
        //Data
        public String HtmlText { get; set; }
        public String Comment { get; set; }
        //Metadata
        public DateTime CreationTime { get; set; }
        public string Translator { get; set; }

             */
            // Doing left joins in Linq is not pretty. Google it
            // I want a list with pairs of webtexts for a View. It has to be a left join since
            //The too language might be missing. Eg for view home token welcome en is: Hello but the there is no tuple for italian welcome.
            List<WebTextCombined> viewSearchReturn = null;
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                var webTextCombinedList = from webTextFromLang in RavenSession.Query<WebText>()
                    join webTextToLang in RavenSession.Query<WebText>()
                        on new WebText(null){View=webTextFromLang.View,Name=webTextFromLang.Name,Lang = rightLang} equals  new WebText(null){View=webTextToLang.View,Name=webTextToLang.Name,Lang = leftLang}                                                                      
                        into tempWebText
                                          from webTextR in tempWebText.DefaultIfEmpty
                                          (new WebText(String.Empty)
                                          {
                                              View = webTextFromLang.View,
                                              Name = webTextFromLang.Name,
                                              Lang = leftLang,
                                              HtmlText = String.Empty,
                                              Comment = String.Empty,
                                              Translator = String.Empty
                                          })
                                  select new WebTextCombined
                                  {
                                      WebTextLeft = webTextFromLang,
                                      WebTextRight = webTextR
                                  };
                viewSearchReturn = webTextCombinedList.ToList();
            }
            return viewSearchReturn;
        }

        public void AddTestData(IDocumentSession session)
        {
            const string VIEW_NAME = "Home";
            const string TRANSLATOR = "MockFake";
            // List<WebText> webTexts = new List<WebText>();
            session.Store(new WebText(TRANSLATOR)
                {
                    View = VIEW_NAME,
                    Lang = "en",
                    Name = "LatestNewsHeader",
                    HtmlText = "Latest News "
                });
            session.Store(new WebText(TRANSLATOR)
                {
                    View = VIEW_NAME,
                    Lang = "en",
                    Name = "slide2Txt2",
                    HtmlText = "The Camp is 3 days with Embu competion"
                });
            session.Store(new WebText(TRANSLATOR)
                {
                    View = VIEW_NAME,
                    Lang = "sv",
                    Name = "LatestNewsHeader",
                    HtmlText = "Senaste nytt <span>Vad har hänt</span>"
                });
            session.Store(new WebText(TRANSLATOR)
                {
                    View = VIEW_NAME,
                    Lang = "sv",
                    Name = "slide2Txt2",
                    HtmlText = "Lägret är på 3 dagar med Embu tävling"
                });

            session.Store(new WebText(TRANSLATOR)
                {
                    View = VIEW_NAME,
                    Lang = "ja",
                    Name = "LatestNewsHeader",
                    HtmlText = "最新ニュース"
                });
            session.Store(new WebText(TRANSLATOR)
                {
                    Lang = "ja",
                    View = VIEW_NAME,
                    Name = "slide2Txt2",
                    HtmlText = "合宿はエンブと3日です"
                });

            session.SaveChanges();
        }
    }
}