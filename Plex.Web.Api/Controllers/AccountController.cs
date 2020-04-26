using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plex.Api;
using Plex.Web.Api.ResourceModels;

namespace Plex.Web.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IPlexClient _plexClient;

        public AccountController(IPlexClient plexClient)
        {
            _plexClient = plexClient;
        }
        
        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> Index(SigninModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }
            var user = await _plexClient.SignIn(model.Username, model.Password);
            return Ok(user);
        }
    }
}