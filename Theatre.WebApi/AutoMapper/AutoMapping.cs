using AutoMapper;
using Theatre.Data.Core.Models;
using Theatre.Dtos.Entities;

namespace Theatre.WebApi.AutoMapper
{

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Spectacle, SpectacleDto>();
            CreateMap<SpectacleDto, Spectacle>();
            CreateMap<SpectacleShortDto, Spectacle>();
            CreateMap<Spectacle, SpectacleShortDto>();

            CreateMap<SpectacleSession, SpectacleSessionDto>();
            CreateMap<SpectacleSessionDto, SpectacleSession>();
            CreateMap<SpectacleSession, SpectacleSessionShortDto>();
            CreateMap<SpectacleSessionShortDto, SpectacleSession>();

            CreateMap<SpectacleSessionReservation, SpectacleSessionReservationDto>();
            CreateMap<SpectacleSessionReservationDto, SpectacleSessionReservation>();
        }
    }
}
