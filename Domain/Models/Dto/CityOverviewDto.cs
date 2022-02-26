using Infrastructure.Entity.TrainGovernor;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Dto
{
    public class CityOverviewDto
    {
        [Required(ErrorMessage = "Id cannot be null")]
        public int Id { get; set; }

        [Required(ErrorMessage = "City cannot have no name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City name must exceed one character")]
        public string Name { get; set; }

        public string? ZipCode { get; set; }
        public bool IsActive { get; set; }

        public List<TrainStationInfoDto> Stations { get; set; }

        public CityOverviewDto(City entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ZipCode = entity.ZipCode;

            IsActive = entity.IsActive;
        }

        public CityOverviewDto() { }

        public City ToEntity()
        {
            return new City()
            {
                Id = Id,
                Name = Name,
                ZipCode = ZipCode,
                //Stations = Stations,
                IsActive = IsActive
            };
        }
    }
}