using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plex.Web.Api.Services;

namespace Plex.Web.Api.Controllers
{
    [Route("api/webhook/")]
    public class WebhookController : Controller
    {
        private readonly IPlexService _plexService;
        private readonly ILogger<WebhookController> _logger;
        
        public WebhookController(IPlexService plexService, ILogger<WebhookController> logger)
        {
            _plexService = plexService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public void GetWebhook([FromBody]string payload)
        { 
            _logger.LogInformation("GetWebhook response captures from");
            
        }
    }
}