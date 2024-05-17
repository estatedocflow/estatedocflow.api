using estatedocflow.api.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace estatedocflow.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;
        public HomeController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _rabbitMQService.SendMessage("Hello RabbitMQ!");  
            return Ok();
        }
    }
}


