using System;
using System.Web.Mvc;
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
    }
}
