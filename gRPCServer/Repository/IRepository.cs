using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IRepository
    {

        public DataResponse GetMemberData(string memberID);
    }
}
