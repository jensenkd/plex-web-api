using System.Collections.Generic;
using System.Threading.Tasks;
using Plex.Api.Models;
using Plex.Api.Models.Status;
using Plex.Web.Api.ResourceModels;
using Directory = Plex.Api.Models.Directory;

namespace Plex.Web.Api.Services
{
    public interface IPlexService
    {
        // Libraries
        Task<List<Directory>> GetLibraries(string authKey, string plexServerHost);

        Task<List<Metadata>> GetLibraryItems(string authKey, string plexServerHost, string libraryKey);
        Task<List<Metadata>> GetRandomMovies(string authKey, string plexServerHost, string[] libraryKeys, int numberOfMovies = 5);
        Task<List<Session>> GetActiveSessions(string authKey, string plexServerHost);
        Task<Session> GetActiveSessionForPlayer(string authKey, string plexServerHost, string playerMachineId);
    }
}