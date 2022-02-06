using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using Assert = Xunit.Assert;


namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void ConfiguredAction_Returns_string_value()
        {
            const string id = "123";
            const string value_1 = "QWE";
            const string expected_string = $"Hello World! {id} - {value_1}";

            var controller = new HomeController();

            var actual_string = controller.ConfiguredAction(id, value_1);

            Assert.Equal(expected_string, actual_string);
        }
    }
}
