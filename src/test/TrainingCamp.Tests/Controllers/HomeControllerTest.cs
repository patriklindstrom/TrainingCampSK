using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingCamp.Web;
using TrainingCamp.Web.Controllers;

namespace TrainingCamp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();
            var fakeRepo = new WebTextRepoMock();
            controller.WebTextRepo = fakeRepo;
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Shorjini Kempo Camp Stockholm 2014 homepage", result.ViewBag.Message);
        }

       
    }
}
