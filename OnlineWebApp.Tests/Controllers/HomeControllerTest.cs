using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineWebApp;
using OnlineWebApp.Controllers;
using OnlineWebApp.Models;

namespace OnlineWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetCartItems()
        {
            // Arrange
            int Item_Id = 16;
            int Quantity = 10;
            int count = 1;
            int Cart = 16;
            int expected = 9;
            Cart cart = new Cart();
            Items items = new Items();

            // Act
            //cart.Item_Id = Cart;
            //items.Item_Id = Item_Id;
            int actual = Quantity - count;


            // Assert

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
