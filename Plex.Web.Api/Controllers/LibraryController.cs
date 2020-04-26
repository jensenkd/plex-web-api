using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plex.Api.Models;
using Plex.Web.Api.Services;

namespace Plex.Web.Api.Controllers
{
    [Route("api/libraries/")]
    public class LibraryController : Controller
    {
        private readonly IPlexService _plexService;

        public LibraryController(IPlexService plexService)
        {
            _plexService = plexService;
        }

        [Route("{libraryKey}")]
        [HttpGet]
        public async Task<IActionResult> GetLibrary([Required] string authKey,[Required] string plexServerUrl, string libraryKey)
        {
            MediaContainer library = await _plexService.GetLibrary(authKey, plexServerUrl, libraryKey);

            return Ok(library);
        }
        
        [Route("{libraryKey}/items")]
        [HttpGet]
        public async Task<IActionResult> GetLibraryMetadata([Required] string authKey,[Required] string plexServerUrl, string libraryKey)
        {
            List<Metadata> items = await _plexService.GetLibraryItems(authKey, plexServerUrl, libraryKey);

            return Ok(items);
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetLibraries([Required] string authKey,[Required] string plexServerUrl,
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