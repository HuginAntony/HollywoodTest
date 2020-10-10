using AutoMapper;
using Tournament.Core.Models;
using Tournament.DataAccess.Models;

namespace Tournament.WebApi.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TournamentRequest, DataAccess.Models.Tournament>().ReverseMap();
            CreateMap<EventRequest, Event>().ReverseMap();
            CreateMap<EventDetailRequest, EventDetail>().ReverseMap();

            CreateMap<DataAccess.Models.Tournament, TournamentResponse>();
            CreateMap<Event, EventResponse>();
            CreateMap<EventDetail, EventDetailResponse>();

        }
    }
}