using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API01.Controllers
{
    [Authorize("permission")]
    [Route("api01/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(API01Model), 200)]
        [ProducesResponseType(typeof(IEnumerable<API01Model>), 200)]
        public ActionResult<IEnumerable<API01Model>> Get()
        {
            return new API01Model[] {
                new API01Model { ID=1, IsSure=true, Price=2.3m, Describe="test1" },
                new API01Model { ID=2, IsSure=true, Price=1.3m, Describe="test2" },
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
