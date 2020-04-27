using Plex.Api.Models;
using Plex.Api.Models.Status;

namespace Plex.Web.Api.ResourceModels
{
    // AutoMapping.cs
    using AutoMapper;
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Session, SessionModel>();

            CreateMap<Metadata, MovieModel>();
        }
    }
}