using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}