using estatedocflow.api.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace estatedocflow.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IRabbitMQService rabbitMqService) : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            rabbitMqService.SendMessage("Hello RabbitMQ!");  
            return Ok();
        }
    }
}


