namespace Befer.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
           
        }
        
        public IActionResult Get()
        {
            return Ok("works");
        }
    }
}