using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Tests
{
    class WebTextRepoMock : IWebTextRepo
    {
        public string GetWebTextRepo(int id)
        {
            throw new NotImplementedException();
        }

        public List<WebText> SearchWebTextRepo(string name)
        {
            throw new NotImplementedException();
        }

        public List<WebText> GetAllWebTextRepoForView(string viewName, string lang)
        {
            List<WebText> viewSearchReturn = null;
            switch (lang)
            {
                case "en":
                    viewSearchReturn = new List<WebText>
                        {
                            new WebText
                                {
                                    WebTextId = 1,
                                    View = viewName,
                                    Lang = "en",
                                    Name = "LatestNewsHeader",
                                    HtmlText = "Latest News "
                                },
                            new WebText
                                {
                                    WebTextId = 2,
                                    View = viewName,
                                    Name = "slide2Txt2",
                                    HtmlText = "The Camp is 3 days with Embu competion"
                                }
                        };
                    break;
                case "sv":
                    viewSearchReturn = new List<WebText>
                        {
                            new WebText
                                {
                                    WebTextId = 3,
                                    View = viewName,
                                    Lang = "sv",
                                    Name = "LatestNewsHeader",
                                    HtmlText = "Senaste nytt <span>Vad har hänt</span>"
                                },
                            new WebText
                                {
                                    WebTextId = 4,
                                    View = viewName,
                                    Name = "slide2Txt2",
                                    HtmlText = "Lägret är på 3 dagar med Embu tävling"
                                }
                        };
                    break;
                case "ja":
                    viewSearchReturn = new List<WebText>
                        {
                            new WebText
                                {
                                    WebTextId = 5,
                                    View = viewName,
                                    Lang = "ja",
                                    Name = "LatestNewsHeader",
                                    HtmlText = "最新ニュース"
                                },
                            new WebText {WebTextId = 6, View = viewName, Name = "slide2Txt2", HtmlText = "合宿はエンブと3日です"}
                        };
                    break;
            }


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
    }
}
