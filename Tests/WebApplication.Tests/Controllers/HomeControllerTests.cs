using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication.Controllers;
using WebApplication.Domain;
using WebApplication.Domain.Entities;
using WebApplication.Interfaces.Services;
using Assert = Xunit.Assert;


namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {

        [TestMethod]
        public void Index_returns_View()
        {
            var product_data_mock = new Mock<IProductData>();
            product_data_mock.Setup(s => s.GetProducts(It.IsAny<ProductFilter>()))
                .Returns<ProductFilter>(f => Enumerable.Empty<Product>());

            var controller = new HomeController();

            var result = controller.Index(product_data_mock.Object);

            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void ConfiguredAction_Returns_string_value()
        {

            #region Arrange

            const string id = "123";
            const string value_1 = "QWE";
            const string expected_string = $"Hello World! {id} - {value_1}";

            var controller = new HomeController();

            #endregion

            #region Act

            var actual_string = controller.ConfiguredAction(id, value_1);

            #endregion

            #region Assert

            Assert.Equal(expected_string, actual_string);

            #endregion
        }
    }
}
