using estatedocflow.api.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace estatedocflow.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        public HomeController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _rabbitMqService.SendMessage("Hello RabbitMQ!");  
            return Ok();
        }
    }
}


