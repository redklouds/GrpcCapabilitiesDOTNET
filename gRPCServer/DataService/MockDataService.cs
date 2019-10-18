using Common.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class MockDataService: DataServiceBase, IDataService
    {
        public MockDataService(IRepository r): base(repo: r)
        {}


        public DataResponse GetMemberData(string memberID)
        {
            DataResponse response = this.repository.GetMemberData(memberID);
            if(response.StatusCode == 200)
            {
                return response;
            }
            return null;
        }
    }
}
