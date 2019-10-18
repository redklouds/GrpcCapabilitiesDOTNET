using DataService;
using Grpc.Core;
using gRPCServer.Protos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCServer.Services
{
    public class MemberService: MemberData.MemberDataBase
    {

        private readonly ILogger<MemberService> _logger;
        private IDataService mockDataService;
        public MemberService(ILogger<MemberService> logger)
        {
            _logger = logger;
            mockDataService = new MockDataService(new MockDataRepository());

        }

        public override Task<GetMemberResponse> GetMemberData(GetMemberRequest request, ServerCallContext context)
        {
            var dr = mockDataService.GetMemberData(request.MemberId);
            GetMemberResponse response = new GetMemberResponse();
            if (dr == null)
            {
                response.StatusCode = 400;
                response.GetSuccessful = false;
            }
            else
            {
                Common.Models.Member m1 = JsonConvert.DeserializeObject<Common.Models.Member>(dr.ResponseBody);
                Member m = new Member
                {
                    FirstName = m1.FirstName,
                    Lastname = m1.LastName,
                    SSN = m1.SSN,
                    City = m1.City,
                    MemberId = m1.MemberID,
                };
                response.Member = m;
                response.StatusCode = 200;
                response.GetSuccessful = true;
            }

            return Task.FromResult(response);

        }

    }
}
