using System;
using System.Collections.Generic;
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
        List<WebText> SearchWebTextRepo(string name);
        List<WebText> GetAllWebTextRepoForView(string viewName, string lang);
        void AddWebText(WebText webText);
        void EditWebText(int webTextId);
    }

    public class WebTextRepoRavenDB : IWebTextRepo
    {
        public IDocumentStore MyDocumentStore { get; set; }
        public IDocumentSession Session { get; set; }

        public WebTextRepoRavenDB()
        {
            //var documentStore = new EmbeddableDocumentStore {RunInMemory = true}.Initialize() ;
            if (MyDocumentStore == null)
            {

                 MyDocumentStore = new DocumentStore
                {
                    Url = "https://ibis.ravenhq.com/databases/LCube-TrainingCampSK",
                    ApiKey = "2a4d9d4f-a5f6-4663-983d-3aa7cbac9bd4"
                };
                MyDocumentStore.Initialize();

                //MyDocumentStore = new EmbeddableDocumentStore { RunInMemory = true }.Initialize();
                //        MyEmbeddableDocumentStore =
                //new EmbeddableDocumentStore { DataDirectory = "~/App_Data/RavenDB"}
                //    .Initialize();
                Session = MyDocumentStore.OpenSession();
            }
        }

        public string GetWebTextRepo(int id)
        {
            throw new NotImplementedException();
        }

        public List<WebText> SearchWebTextRepo(string name)
        {
            var searchReturn = new List<WebText>
                {
                    new WebText {WebTextId = 1, View = "Home", Name = name, HtmlText = "NotDone"}
                };
            return searchReturn;
        }

        public List<WebText> GetAllWebTextRepoForView(string viewName, string lang)
        {
          //  AddTestData(Session);
            List<WebText> viewSearchReturn = null;
            viewSearchReturn = Session.Query<WebText>()
                .Where(t => t.View==viewName && t.Lang==lang)
                .ToList();
            Session.Dispose();
            MyDocumentStore.Dispose();
            return viewSearchReturn;
        }

        public void AddWebText(WebText webText)
        {
            throw new NotImplementedException();
        }

        public void EditWebText(int webTextId)
        {
            throw new NotImplementedException();
        }

        public void AddTestData(IDocumentSession session)
        {
     
            const string viewName = "Home";
            // List<WebText> webTexts = new List<WebText>();
            session.Store(new WebText
                {
                    WebTextId = 1,
                    View = viewName,
                    Lang = "en",
                    Name = "LatestNewsHeader",
                    HtmlText = "Latest News "
                });
            session.Store(new WebText
                {
                    WebTextId = 2,
                    View = viewName,
                    Lang = "en",
                    Name = "slide2Txt2",
                    HtmlText = "The Camp is 3 days with Embu competion"
                });
            session.Store(new WebText
                {
                    WebTextId = 3,
                    View = viewName,
                    Lang = "sv",
                    Name = "LatestNewsHeader",
                    HtmlText = "Senaste nytt <span>Vad har hänt</span>"
                });
            session.Store(new WebText
                {
                    WebTextId = 4,
                    View = viewName,
                    Lang = "sv",
                    Name = "slide2Txt2",
                    HtmlText = "Lägret är på 3 dagar med Embu tävling"
                });

            session.Store(new WebText
                {
                    WebTextId = 5,
                    View = viewName,
                    Lang = "ja",
                    Name = "LatestNewsHeader",
                    HtmlText = "最新ニュース"
                });
            session.Store(new WebText { WebTextId = 6, Lang = "ja", View = viewName, Name = "slide2Txt2", HtmlText = "合宿はエンブと3日です" });
            session.SaveChanges();
        }
    }
}