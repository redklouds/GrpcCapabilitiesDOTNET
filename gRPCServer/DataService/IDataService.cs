using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public interface IDataService
    {

        public DataResponse GetMemberData(string memberId);
    }
}
