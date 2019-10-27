using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RESTServer.Controllers;
using System.Net;

namespace RESTServer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private ILogger<MemberDataController> log;

        MemberDataController memberDataController;

        [SetUp]
        public void Setup()
        {

            memberDataController = new MemberDataController(null);
            //Assert.Pass();
        }

        [TestMethod]
        public void TestGetMemberData()
        {
            Setup();
            ActionResult result = memberDataController.getMemberData(new Common.Models.DataRequest
            {
                MemberID = null
            });
            NUnit.Framework.Assert.AreEqual(result.GetType(), typeof(BadRequestObjectResult));

        }

        [TestMethod]
        public void TestMethod1()
        {


            var controller = new MemberDataController(log);

            ActionResult  result = controller.getMemberData(new Common.Models.DataRequest
            {
                MemberID = null
            });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result.GetType(), typeof(BadRequestObjectResult));
        }
    }
}
