using NUnit.Framework;
using gRPCServer;
using gRPCServer.Services;

using System.Threading.Tasks;
//using gRPCServer.Protos;
using Common.Models.Grpc.Protos;
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
            GetMemberResponse result = await _memberService.GetMemberData(new GetMemberRequest
            {
                MemberId = "6969"
            }, null);




        }
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}