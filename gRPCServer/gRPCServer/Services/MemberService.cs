using DataService;
using Grpc.Core;
//using gRPCServer.Protos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Common.Models.Grpc.Protos;

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
                Member m1 = JsonConvert.DeserializeObject<Member>(dr.ResponseBody);
  
                response.Member = m1;
                response.StatusCode = 200;
                response.GetSuccessful = true;
            }

            return Task.FromResult(response);

        }

    }
}
