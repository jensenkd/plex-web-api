using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plex.Web.Api.Services;

namespace Plex.Web.Api.Controllers
{
    [Route("api/sessions/")]
    public class SessionController : Controller
    {
        private readonly IPlexService _plexService;

        public SessionController(IPlexService plexService)
        {
            _plexService = plexService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSessions([Required] string authKey,[Required] string plexServerUrl, string playerMachineId)
        {
            if (string.IsNullOrEmpty(authKey) || string.IsNullOrEmpty(plexServerUrl))
            {
                return BadRequest();
            }
            
            if (string.IsNullOrEmpty(playerMachineId))
            {
                return Ok(await _plexService.GetActiveSessionForPlayer(authKey, plexServerUrl, playerMachineId));
            }
            
            return Ok(await _plexService.GetActiveSessions(authKey, plexServerUrl));
        }
    }
}