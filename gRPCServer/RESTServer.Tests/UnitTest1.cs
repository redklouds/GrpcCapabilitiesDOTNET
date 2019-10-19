using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTServer.Controllers;
using System.Net;

namespace RESTServer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private ILogger<MemberDataController> log;

        [TestMethod]
        public void TestMethod1()
        {


            var controller = new MemberDataController(log);

            ActionResult  result = controller.getMemberData(new Common.Models.DataRequst
            {
                MemberID = null
            });
            Assert.AreEqual(result, typeof(BadRequestResult));
        }
    }
}
