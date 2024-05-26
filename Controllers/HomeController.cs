using estatedocflow.api.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace estatedocflow.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;
        public HomeController(IRabbitMQService rabbitMqService)
        {
            _rabbitMQService = rabbitMqService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _rabbitMQService.SendMessage("Hello RabbitMQ!");  
            return Ok();
        }
    }
}


