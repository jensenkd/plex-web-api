using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Plex.Api;
using Plex.Api.Models;
using Plex.Api.Models.Status;
using Plex.Web.Api.ResourceModels;
using Directory = Plex.Api.Models.Directory;

namespace Plex.Web.Api.Services
{
    public class PlexService : IPlexService
    {
        private readonly IPlexClient _plexClient;
        private readonly IMapper _mapper;

        public PlexService(IPlexClient plexClient, IMapper mapper)
        {
            _plexClient = plexClient;
            _mapper = mapper;
        }

        public async Task<SessionModel> GetActiveSession(string authKey, string plexServerHost, string playerMachineId)
        {
            List<Session> sessions = await _plexClient.GetSessions(authKey, plexServerHost);
            if (sessions == null || sessions.Count == 0)
            {
                return null;
            }
            
            return _mapper.Map<SessionModel>(sessions.FirstOrDefault(c => 
                    c.Player.MachineIdentifier == playerMachineId));
        }

        public async Task<List<Directory>> GetLibraries(string authKey, string plexServerHost)
        {
            var container = await _plexClient.GetLibraries(authKey, plexServerHost);

            return container?.MediaContainer?.Directory;
        }

        public async Task<List<Metadata>> GetLibraryItems(string authKey, string plexServerHost, string libraryKey)
        {
            var container = await _plexClient.MetadataForLibrary(authKey, plexServerHost, libraryKey);

            if (container)
                
            return container?.MediaContainer?.Metadata;
        }

        public async Task<List<Metadata>> GetRandomMovies(string authKey, string plexServerHost, string[] libraryKeys, int numberOfMovies = 1)
        {
            PlexMediaContainer libraries = await _plexClient.GetLibraries(authKey, plexServerHost);

            var directories = libraries.MediaContainer.Directory.
                Where(c => libraryKeys.Contains(c.Key, StringComparer.OrdinalIgnoreCase));

            List<Metadata> movies = new List<Metadata>();
            foreach (var directory in directories)
            {
                var items = await _plexClient.GetLibrary(authKey, plexServerHost, directory.Key);
                movies.AddRange(items.MediaContainer.Metadata);
            }
            
            Random rnd = new Random();
            var randomMovies = movies.OrderBy(x => rnd.Next())
                .Take(numberOfMovies)
                .ToList();

            return randomMovies;
           // return _mapper.Map<List<MovieModel>>(randomMovies);
        }

        public async Task<List<Session>> GetActiveSessions(string authKey, string plexServerHost)
        {
            List<Session> sessions = await _plexClient.GetSessions(authKey, plexServerHost);
            if (sessions == null || sessions.Count == 0)
            {
                return null;
            }

            return sessions;
        }

        public async Task<Session> GetActiveSessionForPlayer(string authKey, string plexServerHost, string playerMachineId)
        {
            List<Session> sessions = await _plexClient.GetSessions(authKey, plexServerHost);
            if (sessions == null || sessions.Count == 0)
            {
                return null;
            }
            
            return sessions.FirstOrDefault(c => 
                string.Equals(c.Player.MachineIdentifier, playerMachineId, StringComparison.OrdinalIgnoreCase));
        }
    }
}