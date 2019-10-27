using NUnit.Framework;
using gRPCServer;
using gRPCServer.Services;

using System.Threading.Tasks;
//using gRPCServer.Protos;
using Common.Models.Grpc.Protos;
using System.Collections.Generic;

namespace gRPCServer.Tests
{
    public class Tests
    {
        private MemberService _memberService;
        [SetUp]
        public void Setup()
        {

            _memberService = new MemberService(null);

            //Assert.Pass();
        }
            
        [Test]
        public async Task TestGetMemberData()
        {
            //this will be testing the Mock Data Service and return a mock member object
            Member testMember = new Member
            {
                FirstName = "Danny",
                Lastname = "Ly",
                City = "Seattle",
                MemberId = "123456789",
                SSN = "1234567899",
                State = "Washington",
                Age = 32,
                Street = "BakerView Rd.",
                BloodType = "A",
                Occupation = "Software Engineer",
                Salary = 150000,

            };
            List<Case> cases = new List<Case>
            {
                new Case
                {
                    CaseId = "ABCDEFG",
                    Subject = "Check Up",
                    Title  = "Title",
                }
            };
            testMember.Cases.Add(cases);

            GetMemberResponse result = await _memberService.GetMemberData(new GetMemberRequest
            {
                MemberId = "123456789"
            }, null);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Member);
            
            Assert.AreEqual(testMember, result.Member);




        }
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}