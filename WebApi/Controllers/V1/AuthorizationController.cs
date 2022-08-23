using Bussines.Abstraction.Authorization.Models.Input;
using Data.MoackData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;
using WebApi.Models;
using IAuthorizationService = Bussines.Abstraction.Authorization.Interfaces.IAuthorizationService;

namespace WebApi.Controllers.V1
{
    [Roles(CustomRoles.Admin)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AuthorizationController : BaseController
    {
        private readonly IAuthorizationService _authService;

        public AuthorizationController(IAuthorizationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public override IActionResult CheckApiVersioning(string apiVersiong)
        {
            return base.CheckApiVersioning("1.0");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredentialsDto credentials)
        {
            var result = _authService.Login(credentials);
            return Ok(result);
        }
    }
}
