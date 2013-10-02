using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Database.Indexing;

namespace TrainingCamp.Web.Repository
{
    public interface IWebTextRepo
    {
        WebText GetWebTextRepo(string id);
        void StoreWebText(WebText webText);
        List<WebText> SearchWebText(string name);
        List<WebText> SearchWebText(string viewName, string lang);
        List<WebText> SearchWebText(string viewName, string lang,string name);
        Boolean WebTextExist(string viewName, string lang, string name);
        void AddWebText(WebText webText);
        List<WebTextCombinedLight> SearchWebTextLeftJoin(string viewName, string rightLang, string leftLang);
        void UpdateWebText(WebText tLWebText);
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


        public WebText GetWebTextRepo(string id)
        {
            WebText wt = null;
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                 wt = RavenSession.Load<WebText>(id);            
            }
            return wt;
        }

        public void StoreWebText(WebText webText)
        {
           
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                Debug.Assert(webText != null, "webText != null");
                webText.CreationTime = DateTime.Now;
                webText.Translator=webText.Translator ?? "Unknown";
                RavenSession.Store(webText);
                RavenSession.SaveChanges();
            }
            
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

       

        public void UpdateWebText(WebText tLWebText)
        {
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
                Debug.Assert(tLWebText != null, "webText != null");
                tLWebText.CreationTime = DateTime.Now;
                tLWebText.Translator = tLWebText.Translator ?? "Unknown";
                RavenSession.Store(tLWebText);
                RavenSession.SaveChanges();
            }
        }

        public List<WebTextCombinedLight> SearchWebTextLeftJoin(string viewName, string rightLang, string leftLang)
        {
            List<WebTextCombinedLight> viewSearchReturn = null;
            IQueryable<WebTextCombinedLight> wt;
           
            using (RavenSession)
            {
                Debug.Assert(RavenSession != null, "RavenSession != null");
           if (leftLang == "sv")
            {
                wt = RavenSession.Query<WebTextCombinedLight, LeftJoinPageTextElementEnSv>().Where(x => x.View == viewName); 
            }
            else
            {
                 wt = RavenSession.Query<WebTextCombinedLight, LeftJoinPageTextElementEnJp>().Where(x => x.View == viewName); 
            }
              
                
                
            }
            viewSearchReturn = wt.ToList();
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