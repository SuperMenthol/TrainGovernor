using Infrastructure.Entity.TrainGovernor;
using Domain.Models.ValueObjects.Primitives;

namespace Domain.Models.Dto
{
    public class TrainStationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public bool IsActive { get; set; }
        public string StreetAddress { get; set; }

        public Address Address { get; set; }

        public TrainStationDto(TrainStation station)
        {
            Id = station.Id;
            Name = station.Name;
            ZipCode = station.ZipCode;
            CityId = station.CityId;
            IsActive = station.IsActive;
            StreetAddress = station.Address;
            Address = new Address(ZipCode, CityId, station.Address);
        }

        public TrainStationDto()
        {

        }

        public TrainStation ToEntity()
        {
            return new TrainStation()
            {
                Id = Id,
                Name = Name,
                ZipCode = ZipCode,
                CityId = CityId,
                IsActive = IsActive,
                Address = Address.StreetAddress
            };
        }
    }
}