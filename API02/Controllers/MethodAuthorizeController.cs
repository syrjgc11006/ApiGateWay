using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API02.Controllers
{
   
    [ApiController]
    public class MethodAuthorizeController : ControllerBase
    {
        [Route("methodAuthorize/GetUserName")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetUserName()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("methodAuthorize/GetUserNameAuthorize")]
        [HttpGet]
        [Authorize("permission")]
        public ActionResult<string> GetUserNameAuthorize()
        {
            return "assss";
        }
    }
}