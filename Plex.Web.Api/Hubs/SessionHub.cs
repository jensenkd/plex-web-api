using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Plex.Api.Models;
using Plex.Web.Api.ResourceModels;
using Plex.Web.Api.Services;

namespace Plex.Web.Api.Hubs
{
    public class SessionHub : Hub
    { 
        private readonly IPlexService _plexService;

        public SessionHub(IPlexService plexService)
        {
            _plexService = plexService;
        }
        
        public decimal GetProgressPercent(decimal duration, decimal offset)
        {
            int durint = (int) duration;
            int time = durint / 1000;
            decimal days = Math.Floor((decimal) (time / (24 * 60 * 60)));

            var hours = Math.Floor((time - (days * 24 * 60 * 60)) / (60 * 60));
            var minutes = Math.Floor((time - (days * 24 * 60 * 60) - (hours * 60 * 60)) / 60);
            var seconds = (time - (days * 24 * 60 * 60) - (hours * 60 * 60) - (minutes * 60)) % 60;

            //offSet = $clients['viewOffset'];
            var offint = (int) offset;
            var timeoff = offint / 1000;
            var daysoff = Math.Floor((decimal) timeoff / (24 * 60 * 60));
            var hoursoff = Math.Floor((timeoff - (daysoff * 24 * 60 * 60)) / (60 * 60));
            var minutesoff = Math.Floor((timeoff - (daysoff * 24 * 60 * 60) - (hoursoff * 60 * 60)) / 60);
            var secondsoff = (timeoff - (daysoff * 24 * 60 * 60) - (hoursoff * 60 * 60) - (minutesoff * 60)) % 60;

            decimal percentComplete = (timeoff / time) * 100;

            return percentComplete;
        }

        public async Task<SessionModel> InitiateSession(string authKey, string serverHost, string playerId, string[] movieLibraries, int delaySeconds = 30)
        {
            const int plexSessionDelayMs = 5000;
            var serverHostFullUri = serverHost.TrimEnd('/');
            
            while (true)
            {
                var session = await _plexService.GetActiveSessionForPlayer(authKey, serverHostFullUri, playerId);

                if (session?.Player != null && (string.Equals(session.Type, "movie", StringComparison.OrdinalIgnoreCase) ||
                                                string.Equals(session.Type, "episode", StringComparison.OrdinalIgnoreCase)))
                {
                    var sessionModel = new SessionModel
                    {
                        PlayerState = session.Player.State,
                        Duration = session.Duration,
                        PercentComplete = decimal.Parse(session.ViewOffset) / decimal.Parse(session.Duration),
                        Year = session.Year,
                        Type = session.Type
                    };

                    if (string.Equals(session.Type, "movie", StringComparison.OrdinalIgnoreCase))
                    {
                        sessionModel.Title = session.Title;
                        sessionModel.ArtUrl = Path.Join(serverHostFullUri, session.Art.TrimEnd('/'), "?X-Plex-Token=" + authKey);
                        sessionModel.PosterUrl = Path.Join(serverHostFullUri, session.Thumb.TrimEnd('/'), "?X-Plex-Token=" + authKey);
                    }
                    else
                    {
                        sessionModel.Title = session.GrandparentTitle + " : " + session.ParentTitle + " : " + session.Title;
                        sessionModel.ArtUrl = Path.Join(serverHostFullUri, session.GrandparentArt.TrimEnd('/'), "?X-Plex-Token=" + authKey);
                        sessionModel.PosterUrl = Path.Join(serverHostFullUri, session.GrandparentThumb.TrimEnd('/'), "?X-Plex-Token=" + authKey);
                    }
                    
                    await Clients.Caller.SendAsync("ReceiveSession", sessionModel);
                    Thread.Sleep(plexSessionDelayMs);
                }
                else
                {
                    // Pull random movie poster from Plex
                    var movies = await _plexService.GetRandomMovies(authKey, serverHostFullUri, movieLibraries, 1);

                    Metadata movie = movies.FirstOrDefault();
                    
                    var randomPosterModel = new SessionModel
                    {
                        PlayerState = "none",
                        Title = movie.Title,
                        ArtUrl = Path.Join(serverHostFullUri, movie.Art.TrimEnd('/'), "?X-Plex-Token=" + authKey),
                        PosterUrl = Path.Join(serverHostFullUri, movie.Thumb.TrimEnd('/'), "?X-Plex-Token=" + authKey),
                        Duration = movie.Duration.ToString(),
                        Year = movie.Year,
                        Type = movie.Type
                    };
                    await Clients.Caller.SendAsync("ReceiveSession", randomPosterModel);
                    Thread.Sleep(delaySeconds * 1000);
                }
            }
        }
    }
}