using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Base
{
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public virtual IActionResult CheckApiVersioning(string apiVersion)
        {
            return Ok(new { msg = $"string v{apiVersion}"});
        }
    }
}
