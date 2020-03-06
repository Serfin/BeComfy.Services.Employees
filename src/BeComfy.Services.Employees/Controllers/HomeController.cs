using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public IActionResult Get()
            => Ok("BeComfy Employees Microservice");
    }
}