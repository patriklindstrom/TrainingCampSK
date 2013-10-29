using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingCamp.Web.Controllers;
using TrainingCamp.Web.Repository;

namespace TrainingCamp.Tests.Controllers
{
    [TestClass]
    public class TranslateControllerTest
    {
        [TestMethod]
        public void Test_RavenDB_SearchWebTextLeftJoin()
        {
            // Arrange
            TranslateController controller = new TranslateController();
            var realRepo = new WebTextRepoRavenDB();
            controller.WebTextRepo = realRepo;
            string controllName = "Home";
            string actionName = "Index";
            string langName = "jp";
            string fromLang = "en";
            string message = "Translate Shorjini Kempo Camp Stockholm 2014 for language " + langName;
            // Act
            ViewResult result = controller.Index(controllName, actionName, langName, fromLang) as ViewResult;

            // Assert
            Assert.AreEqual(message, result.ViewBag.Message);
        }

        [TestMethod]
        public void Get_Bing_Suggestion_Translation()
        {
            //Arrange
            TranslateController controller = new TranslateController();
            ITranslatorRepo translatorRepoMock = new TranslatorRepoMock();
            //Act


            //Assert
        }
        [TestMethod]
        public void Translate_All_For_Japanese()
        {
            //Arrange
            TranslateController controller = new TranslateController();
            //ITranslatorRepo translatorRepoMock = new TranslatorRepoMock();
           // ITranslatorRepo translatorRepo = new TranslatorRepo();
            //controller.translatorRepo = translatorRepo;
            IWebTextRepo webTextRepo = new WebTextRepoMock();
            controller.WebTextRepo = webTextRepo;
            //Act
            List<WebText> translatedWebText = controller.TranslateWebTexts("en", "sv");
           // controller.TranslateAll("en", "sv", "Home");

            //Assert
          Assert.AreEqual(translatedWebText[0].HtmlText,"Senaste nytt");  
        }

        [TestMethod]
        public void Get_ReqBody_XmlDoc()
        {
            //Arrange
            IWebTextRepo webTextRepo = new WebTextRepoMock();
            ITranslatorRepo translatorRepo = new TranslatorRepo();
            string sourceLang   = "en";
            string targetLang = "sv";
            string body = "<TranslateArrayRequest>" +
              "<AppId />" +
              "<From>{0}</From>" +
              "<Options>" +
              " <Category xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
              "<ContentType xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\">{1}</ContentType>" +
              "<ReservedFlags xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
              "<State xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
              "<Uri xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
              "<User xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\" />" +
              "</Options>" +
              "<Texts>" +
              "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">{2}</string>" +
              "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">{3}</string>" +
              "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">{4}</string>" +
              "</Texts>" +
              "<To>{5}</To>" +
              "</TranslateArrayRequest>";
            List<WebText> webTextList = webTextRepo.ListWebTextForLang(language: sourceLang);
            string reqBody = string.Format(body, sourceLang, "text/plain", webTextList[0].HtmlText, webTextList[1].HtmlText, webTextList[2].HtmlText, targetLang);
            
            //Act
            //XDocument xbody = new XDocument(
            //    new XElement("TranslateArrayRequest",
            //        new XElement("AppId"),
            //        new XElement("From", sourceLang),
            //        new XElement("Options",
            //            new XElement("Category",
            //                new XAttribute("xmlns",
            //                    "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2\\")
            //                )
            //            )
            //        )
            //    );
            //XDocument xbody = 
            //  new XDocument(
            //        new XElement("TranslateArrayRequest",
            //                new XElement("AppId"),
            //                new XElement("From",sourceLang),
            //                new XElement("Options",
            //                    new XElement("Category", 
            //                        new XAttribute("xmlns", "foo")
            //                        )                      
            //                )
            //        )
            //  );
            XNamespace catNs = "http://www.adventure-works.com";
            XDocument xbody =
                new XDocument(
                    new XElement("TranslateArrayRequest", 
                        new XElement("AppId"),
                        new XElement("From",sourceLang),
                        new XElement("Options",
                        new XElement("Category", new XAttribute(XNamespace.Xmlns +"catNS", catNs))
                         ))
                         );
            string fooXml = xbody.ToString();
            var xmlReqBody = translatorRepo.ReqBody(webTextList, sourceLang, targetLang);
            //Assert
            Assert.AreEqual(xbody.ToString(), reqBody,"The xml document in the translation for array are not same as the hardcoded");
        }
    }
}
