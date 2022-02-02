using AutoMapper;
using Domain.Models.Dto;
using Infrastructure.Entity.TrainGovernor;

namespace Application.MapperConfigurations
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityOverviewDto>();
            CreateMap<TrainStation, TrainStationInfoDto>();
            CreateMap<NeighbouringTrainStation, NeighbouringTrainStationDto>();
            CreateMap<Line, LineDto>();
            CreateMap<LineStation, LineStationDto>();
        }
    }
}