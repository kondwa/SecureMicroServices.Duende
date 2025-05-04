using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Two.Models;

namespace Service.Two.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoController : ControllerBase
    {
        [HttpPost]
        [Route("write")]
        [Authorize(Policy ="WritePolicy")]
        public ActionResult Write([FromBody] WriteRequestDto dto)
        {
            return Ok($"Service Two Write: {dto.Data}");
        }
    }
}
