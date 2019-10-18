using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    class MockDataService: DataServiceBase, IDataService
    {
        public MockDataService(IRepository r): base(repo: r)
        {}


        public void GetMemberData(string memberID)
        {
            this.repository
        }
    }
}
