using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    class MockDataRepository: IRepository
    {

        public MockDataRepository() { }

        public DataResponse GetMemberData(string memberId)
        {
            Member testMbr = new Member
            {
                FirstName = "Dante",
                LastName = "Ly",
                City = "Seattle",
                MemberID = "69696969",
                SSN = "242342"
            };

            //TODO other things such as getting data from somewhere else lol

            DataResponse dr = new DataResponse
            {
                ResponseBody = JsonConvert.SerializeObject(testMbr),
                StatusCode = 200

            };
            return dr;
        }
    }
}
