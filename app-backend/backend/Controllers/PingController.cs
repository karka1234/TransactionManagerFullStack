using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
