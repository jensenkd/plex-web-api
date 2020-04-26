using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plex.Api.Models;
using Plex.Web.Api.Services;

namespace Plex.Web.Api.Controllers
{
    [Route("api/servers/")]
    public class ServerController : Controller
    {
        private readonly IPlexService _plexService;

        public ServerController(IPlexService plexService)
        {
            _plexService = plexService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetServers(string authKey)
        {
            var servers = await _plexService.GetServers(authKey);
            return Ok(servers);
        }
        
        [HttpGet]
        [Route("{serverKey}")]
        public async Task<IActionResult> GetServers(string authKey, string serverKey)
        {
            var server = await _plexService.GetServer(authKey, serverKey);
            return Ok(server);
        }
        
    }
}