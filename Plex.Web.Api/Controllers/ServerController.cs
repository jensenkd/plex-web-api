using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plex.Api;
using Plex.Api.Models;
using Plex.Web.Api.Services;

namespace Plex.Web.Api.Controllers
{
    [Route("api/servers/")]
    public class ServerController : Controller
    {
        private readonly IPlexService _plexService;
        private readonly IPlexClient _plexClient;

        public ServerController(IPlexService plexService, IPlexClient plexClient)
        {
            _plexService = plexService;
            _plexClient = plexClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetServers([Required] string authKey, string serverKey)
        {
            if (string.IsNullOrEmpty(serverKey))
            {
                var servers = await _plexClient.GetServers(authKey);
                return Ok(servers);
            }
            else
            {
                var servers = await _plexClient.GetServers(authKey);
                return Ok(servers?.SingleOrDefault(c =>
                    string.Equals(c.MachineIdentifier, serverKey, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }
}