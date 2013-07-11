using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

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