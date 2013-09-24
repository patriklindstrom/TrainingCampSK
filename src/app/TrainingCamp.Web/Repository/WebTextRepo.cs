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
            List<WebTextCombined> viewSearchReturn = null;
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                var wt = RavenSession.Query<WebTextCombined, LeftJoinPageTextElement>();                
                viewSearchReturn = wt.ToList();
            }
            return viewSearchReturn;
        }

        public void AddTestData(IDocumentSession session)
        {
            const string VIEW_NAME = "Home";
            const string TRANSLATOR = "MockFake";
            // List<WebText> webTexts = new List<WebText>();
            session.Store(new WebText()
                {
                    View = VIEW_NAME,
                    Lang = "en",
                    Name = "LatestNewsHeader",
                    HtmlText = "Latest News ",
                    Translator = TRANSLATOR
                });
            session.Store(new WebText()
                {
                    View = VIEW_NAME,
                    Lang = "en",
                    Name = "slide2Txt2",
                    HtmlText = "The Camp is 3 days with Embu competion",
                    Translator = TRANSLATOR
                });
            session.Store(new WebText()
                {
                    View = VIEW_NAME,
                    Lang = "sv",
                    Name = "LatestNewsHeader",
                    HtmlText = "Senaste nytt <span>Vad har hänt</span>",
                    Translator = TRANSLATOR
                });
            session.Store(new WebText()
                {
                    View = VIEW_NAME,
                    Lang = "sv",
                    Name = "slide2Txt2",
                    HtmlText = "Lägret är på 3 dagar med Embu tävling",
                    Translator = TRANSLATOR
                });

            session.Store(new WebText()
                {
                    View = VIEW_NAME,
                    Lang = "ja",
                    Name = "LatestNewsHeader",
                    HtmlText = "最新ニュース",
                    Translator = TRANSLATOR
                });
            session.Store(new WebText()
                {
                    Lang = "ja",
                    View = VIEW_NAME,
                    Name = "slide2Txt2",
                    HtmlText = "合宿はエンブと3日です",
                    Translator = TRANSLATOR
                });

            session.SaveChanges();
        }
    }
}