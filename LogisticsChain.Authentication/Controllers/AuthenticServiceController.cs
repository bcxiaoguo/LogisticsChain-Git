using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LogisticsChain.Entity.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ocelot.JWTAuthorizePolicy;

namespace LogisticsChain.AuthenticService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticServiceController : ControllerBase
    {

        /// <summary>
        /// 自定义策略参数
        /// </summary>
        PermissionRequirement _requirement;
        public AuthenticServiceController(PermissionRequirement requirement)
        {
            _requirement = requirement;
        }
        //[AllowAnonymous]
        //[HttpPost("/Authentication/AuthenticService")]
        //[HttpGet]
        [HttpPost]
        public IActionResult Login([FromBody]LoginEntity loginEntity)
        {
            var isValidated = (loginEntity.username == "gsw" && loginEntity.password == "111111") || (loginEntity.username == "ggg" && loginEntity.password == "222222");
            var role = loginEntity.username == "gsw" ? "admin" : "system";
            if (!isValidated)
            {
                return new JsonResult(new
                {
                    Status = false,
                    Message = "认证失败"
                });
            }
            else
            {
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new Claim[] { new Claim(ClaimTypes.Name, loginEntity.username), new Claim(ClaimTypes.Role, role), new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims, _requirement);
                return new JsonResult(token);
            }
        }

        //[HttpGet]
        //public IActionResult Login(string username)
        //{
        //    var isValidated = (username == "gsw") || (username == "ggg");
        //    var role = username == "gsw" ? "admin" : "system";
        //    if (!isValidated)
        //    {
        //        return new JsonResult(new
        //        {
        //            Status = false,
        //            Message = "认证失败"
        //        });
        //    }
        //    else
        //    {
        //        //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
        //        var claims = new Claim[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, role), new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
        //        //用户标识
        //        var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
        //        identity.AddClaims(claims);

        //        var token = JwtToken.BuildJwtToken(claims, _requirement);
        //        return new JsonResult(token);
        //    }
        //}



        //验证用户名密码  接收用户名 加密的密码 客户端的签名 时间戳 返回Token 从Redis缓存
        //颁发凭证Token
        //销毁
        //登陆
        //登出

    }
    public class LoginEntity
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
