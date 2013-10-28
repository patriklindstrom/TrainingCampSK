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
                        },
                         new WebText()
                        {
                            View = viewName,
                            Name = "slide2Txt3",
                            Lang = "en",
                            HtmlText = "There is only one way to find out",
                            Translator = TRANSLATOR
                        }
                        ,
                         new WebText()
                        {
                            View = viewName,
                            Name = "Footer",
                            Lang = "en",
                            HtmlText = "Shorinji Kempo is a system of \"self-defense and training\"",
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
                        },
                        new WebText()
                        {
                            View = viewName,
                            Name = "slide2Txt3",
                            Lang = "sv",
                            HtmlText = "Sh.",
                            Translator = TRANSLATOR
                        }
                        ,
                                           new WebText()
                        {
                            View = viewName,
                            Name = "Footer",
                            Lang = "sv",
                            HtmlText = "Shorinji Kempo är ett system för \"självförsvar och träning\"",
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
                        },
                       
                        new WebText()
                        {
                            View = viewName,
                            Name = "slide2Txt3",
                            Lang = "ja",
                            HtmlText = "見つけるための唯一の方法があります。",
                            Translator = TRANSLATOR
                        },
                         new WebText()
                        {
                            View = viewName,
                            Name = "Footer",
                            Lang = "ja",
                            HtmlText = " 少林寺拳法は「自衛隊・ トレーニング」のシステム。",
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

        public void UpdateWebText(WebText tLWebText)
        {
            throw new NotImplementedException();
        }

        public List<WebText> ListWebTextForLang(string language)
        {
            return SearchWebText("Home", language);
        }

        public void StoreWebTexts(List<WebText> translatedWebTexts)
        {
            throw new NotImplementedException();
        }
    }
}