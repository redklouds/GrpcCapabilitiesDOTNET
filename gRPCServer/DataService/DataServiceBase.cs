using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class DataServiceBase
    {
        protected IRepository repository;
       // protected  ILogger<IDataService> _logger;
        protected DataServiceBase(IRepository repo)
        {
            this.repository = repo;
        }
    }
}
