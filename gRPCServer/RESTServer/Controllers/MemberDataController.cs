using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DataService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;

namespace RESTServer.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class MemberDataController : ControllerBase
    {
        private readonly ILogger<MemberDataController> _logger;

        private IDataService mockDataService;

        public MemberDataController(ILogger<MemberDataController> logger)
        {
            _logger = logger;
            
            mockDataService = new MockDataService(new MockDataRepository());
        }


        [HttpGet]
        [Route("MemberIndex")]
        public ActionResult tester()
        {
            return Ok("This is member Index");
        }
    



        [HttpPost]
        [Route("GetMemberData")]
        public ActionResult getMemberData([FromBody] DataRequst dataRequest)
        {
            //returns a single member data given the member ID
            if(dataRequest == null)
            {
                return BadRequest("Error parsing Data Request");
            }

            if(dataRequest.MemberID == null)
            {
                return BadRequest("Check your parameters");
            }

            DataResponse dr = mockDataService.GetMemberData(dataRequest.MemberID);

            if(dr == null)
            {
                return BadRequest("Check your parameters");
            }
            return Ok(dr);

        }
    }


}