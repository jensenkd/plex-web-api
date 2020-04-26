using System.Collections.Generic;
using System.Threading.Tasks;
using Plex.Api.Models;
using Plex.Api.Models.Server;
using Plex.Api.Models.Status;
using Plex.Web.Api.ResourceModels;
using Directory = Plex.Api.Models.Directory;

namespace Plex.Web.Api.Services
{
    public interface IPlexService
    {
        // Servers
        Task<List<Server>> GetServers(string authKey);
        Task<Server> GetServer(string authKey, string serverKey);
        
        // Libraries
        Task<List<Directory>> GetLibraries(string authKey, string plexServerHost);
        Task<MediaContainer> GetLibrary(string authKey, string plexServerHost, string libraryKey);
        Task<List<Metadata>> GetLibraryItems(string authKey, string plexServerHost, string libraryKey);
        Task<List<Metadata>> GetRandomMovies(string authKey, string plexServerHost, string[] libraryKeys, int numberOfMovies = 5);
        Task<List<Session>> GetActiveSessions(string authKey, string plexServerHost);
        Task<Session> GetActiveSessionForPlayer(string authKey, string plexServerHost, string playerMachineId);
    }
}