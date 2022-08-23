using Data.MoackData.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;
using WebApi.Models;

namespace WebApi.Controllers.V2
{
    [Roles(CustomRoles.User, CustomRoles.Admin)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AuthorizationController : BaseController
    {
        [HttpGet]
        public override IActionResult CheckApiVersioning(string apiVersiong)
        {
            return base.CheckApiVersioning("2.0");
        }
    }
}
