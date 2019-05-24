using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LogisticsChain.PositionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        [HttpPost]
        public IActionResult Login([FromBody] poststr text)
        {
            return null;
        }

        //[HttpPost("/api/login")]
        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    return null;
        //}
        //[HttpPost]
        //public IActionResult Login3(string text)
        //{
        //    return null;
        //}

        //[HttpPost]
        //public IActionResult Login1([FromBody]string text)
        //{
        //    return null;
        //}

        //[HttpPost]
        //public string Login2([FromBody]JObject text)
        //{
        //    return null;
        //}

        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
    public class poststr
    {
      public string username { get; set; }
       public string password { get; set; }
    }
    }
