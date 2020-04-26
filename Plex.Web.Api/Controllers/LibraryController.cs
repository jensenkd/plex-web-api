using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plex.Api.Models;
using Plex.Web.Api.Services;

namespace Plex.Web.Api.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IPlexService _plexService;
        
        public LibraryController(IPlexService plexService)
        {
            _plexService = plexService;
        }
        
        [Route("libraries")]
        [HttpGet]
        public async Task<IActionResult> GetLibraries(string authKey, string plexServerUrl, 
            string[] libraryKeys, string[] types, string[] titles)
        {
            if (string.IsNullOrEmpty(authKey) || string.IsNullOrEmpty(plexServerUrl))
            {
                return BadRequest();
            }

            List<Directory> libraries = await _plexService.GetLibraries(authKey, plexServerUrl);
            
            if (libraryKeys.Any())
            {
                libraries = libraries
                    .Where(c => libraryKeys.Contains(c.Key))
                    .ToList();
            }

            if (types.Any())
            {
                libraries = libraries
                    .Where(c => types.Contains(c.Type, StringComparer.OrdinalIgnoreCase))
                    .ToList();
            }
            
            if (titles.Any())
            {
                libraries = libraries
                    .Where(c => titles.Contains(c.Title, StringComparer.OrdinalIgnoreCase))
                    .ToList();
            }
            
            return Ok(libraries);
        }
    }
}