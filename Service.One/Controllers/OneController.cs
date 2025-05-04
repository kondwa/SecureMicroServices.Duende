using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.One.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneController : ControllerBase
    {
        [HttpGet]
        [Route("read")]
        [Authorize(Policy = "ReadPolicy")]
        public IActionResult Get()
        {
            return Ok("Service One Read");
        }
    }
}
