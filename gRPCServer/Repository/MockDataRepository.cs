using Common.Models;
using Common.Models.Grpc.Protos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class MockDataRepository: IRepository
    {

        public MockDataRepository() { }

        public DataResponse GetMemberData(string memberId)
        {

            var m = new Common.Models.Grpc.Protos.Member
            {
                FirstName = "Swag",
                Lastname = "slappy",
                City = "Seattle",
                MemberId = memberId,
                SSN = "42424",
                State    = "Washington",
                Age = 32,
                Street = "BakerView Rd.",
                BloodType = "A",
                Occupation = "Software Engineer",
                Salary = 150000,
                
                
            };

            List<Case> caseList = new List<Case>();
            for(int i=0; i < 10; i++)
            {
                caseList.Add(
                    new Case
                    {
                        CaseId = "fjadsfajsdfajh342",
                        Subject = "We are looking love",
                        Title = "Seperate from the crowd"
                    });
            };
            m.Cases.Add(caseList);
            /*
            Member testMbr = new Member
            {
                FirstName = "Dante",
                LastName = "Ly",
                City = "Seattle",
                MemberID = memberId,
                SSN = "242342"
            };
            */

            //TODO other things such as getting data from somewhere else lol

            DataResponse dr = new DataResponse
            {
                ResponseBody = JsonConvert.SerializeObject(m),
                StatusCode = 200

            };
            return dr;
        }
    }
}
