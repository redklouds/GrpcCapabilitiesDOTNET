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
                FirstName = "Danny",
                Lastname = "Ly",
                City = "Seattle",
                MemberId = memberId,
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
            m.Cases.Add(cases);
            /*
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
