using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Tests
{
    internal class WebTextRepoMock : IWebTextRepo
    {
        public WebText GetWebTextRepo(string id)
        {
            throw new NotImplementedException();
        }

        public void StoreWebText(WebText webText)
        {
            throw new NotImplementedException();
        }

        public List<WebText> SearchWebText(string name)
        {
            throw new NotImplementedException();
        }

        public List<WebText> SearchWebText(string viewName, string lang)
        {
            const string TRANSLATOR = "MockFake";
            List<WebText> viewSearchReturn = null;
            switch (lang)
            {
                case "en":
                    viewSearchReturn = new List<WebText>
                    {
                        new WebText()
                        {
                            View = viewName,
                            Lang = "en",
                            Name = "LatestNewsHeader",
                            HtmlText = "Latest News ",
                            Translator = TRANSLATOR
                        },
                        new WebText()
                        {
                            View = viewName,
                            Name = "slide2Txt2",
                            Lang = "en",
                            HtmlText = "The Camp is 3 days with Embu competion",
                            Translator = TRANSLATOR
                        }
                    };
                    break;
                case "sv":
                    viewSearchReturn = new List<WebText>
                    {
                        new WebText()
                        {
                            View = viewName,
                            Lang = "sv",
                            Name = "LatestNewsHeader",
                            HtmlText = "Senaste nytt <span>Vad har hänt</span>",
                            Translator = TRANSLATOR
                        },
                        new WebText()
                        {
                            View = viewName,
                            Name = "slide2Txt2",
                            Lang = "sv",
                            HtmlText = "Lägret är på 3 dagar med Embu tävling",
                            Translator = TRANSLATOR
                        }
                    };
                    break;
                case "ja":
                    viewSearchReturn = new List<WebText>
                    {
                        new WebText()
                        {
                            View = viewName,
                            Lang = "ja",
                            Name = "LatestNewsHeader",
                            HtmlText = "最新ニュース",
                            Translator = TRANSLATOR
                        },
                        new WebText()
                        {
                            View = viewName,
                            Name = "slide2Txt2",
                            Lang = "ja",
                            HtmlText = "合宿はエンブと3日です",
                            Translator = TRANSLATOR
                        }
                    };
                    break;
            }


            return viewSearchReturn;
        }

        public List<WebText> SearchWebText(string viewName, string lang, string name)
        {
            throw new NotImplementedException();
        }

        public bool WebTextExist(string viewName, string lang, string name)
        {
            throw new NotImplementedException();
        }

        public void AddWebText(WebText webText)
        {
            throw new NotImplementedException();
        }

        public void EditWebText(int webTextId)
        {
            throw new NotImplementedException();
        }

        public List<WebTextCombinedLight> SearchWebTextLeftJoin(string viewName, string rightLang, string leftLang)
        {
            throw new NotImplementedException();
        }

        public List<WebTextCombined> SearchWebTextLeftJoinUgly(string viewName, string rightLang, string leftLang)
        {
            throw new NotImplementedException();
        }
    }
}