using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocelot.JwtAuthorize;

namespace ApiAuthorize.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("auth/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        readonly ILogger<LoginController> _logger;
        readonly ITokenBuilder _tokenBuilder;

        public LoginController(ITokenBuilder tokenBuilder, ILogger<LoginController> logger)
        {
            _logger = logger;
            _tokenBuilder = tokenBuilder;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel">登录Model</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(BackResult), 200)]
        [HttpPost]
        public BackResult Login([FromBody]LoginModel loginModel)
        {
            _logger.LogInformation($"{loginModel.UserName} login！");
            if (loginModel.UserName == "gsw" && loginModel.Password == "111111")
            {
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, "gsw"),
                    new Claim(ClaimTypes.Role, "admin"),

                };
                //假设3分钟以后过期
                DateTime expiredtime = DateTime.Now.AddMinutes(10);  //过期时间,修改配置文件RequireExpirationTime 属性为 true
                var token = _tokenBuilder.BuildJwtToken(claims,expires: expiredtime);
                _logger.LogInformation($"{loginModel.UserName} login success，and generate token return");
                return new BackResult { Result = true, Data = token };
            }
            else
            {
                _logger.LogInformation($"{loginModel.UserName} login faile");
                return new BackResult
                {
                    Result = false
                };
            }
        }
    }
}
