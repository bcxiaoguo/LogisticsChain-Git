using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsChain.AppService.Controllers
{ // [Route("demoaapi/[controller]")]
  //  [Route("api/[controller]")]

    //[ApiController]    //[Route("AppService/[controller]")]
    [Route("AppService/[controller]")]
    [Authorize("Permission")]
    public class AppServiceController : ControllerBase
    {

        [HttpGet("/AppService/get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "DemoA服务", "请求" };
        }
        [AllowAnonymous]
         [HttpGet("/AppService/denied")]
        public IActionResult Denied()
        {
            return new JsonResult(new
            {
                Status = false,
                Message = "demoaapi你无权限访问"
            });
        }





        //// GET api/values
        ////[HttpGet]
        ////public ActionResult<IEnumerable<string>> Get()
        ////{
        ////    return new string[] { "AppService", "AppService" };
        ////}

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
}
