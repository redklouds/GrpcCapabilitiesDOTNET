using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RESTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberDataController : ControllerBase
    {
        private readonly ILogger<MemberDataController> _logger;

        public MemberDataController(ILogger<MemberDataController> logger)
        {
            _logger = logger;
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

        }
    }


}