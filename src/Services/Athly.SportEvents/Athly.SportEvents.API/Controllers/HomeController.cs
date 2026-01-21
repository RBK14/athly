using Microsoft.AspNetCore.Mvc;

namespace Athly.SportEvents.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            return Ok("Test API");
        }
    }
}
