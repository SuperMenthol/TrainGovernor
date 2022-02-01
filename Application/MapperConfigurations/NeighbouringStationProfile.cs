using AutoMapper;
using Domain.Models.Dto;
using Infrastructure.Entity.TrainGovernor;

namespace Application.MapperConfigurations
{
    public class NeighbouringStationProfile : Profile
    {
        public NeighbouringStationProfile()
        {
            CreateMap<NeighbouringTrainStation, NeighbouringTrainStationDto>();
        }
    }
}