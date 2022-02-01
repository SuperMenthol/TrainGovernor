using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Stations
{
    public class EditModel : PageModel
    {
        ICityController _cityController { get; set; }
        ITrainStationController _stationController { get; set; }

        public IList<CityOverviewDto> Cities { get; set; }
        public IList<NeighbouringTrainStationDto> NeighbouringTrainStations { get; set; }
        public TrainStationDto Station { get; set; }

        public EditModel(ICityController cityController, ITrainStationController stationController)
        {
            _cityController = cityController;
            _stationController = stationController;
        }

        public async Task OnGet([FromRoute] int id)
        {
            Cities = await _cityController.GetCities();
            Station = await _stationController.GetStation(id);
            NeighbouringTrainStations = await _stationController.GetNeighbouringTrainStations(id);
        }
    }
}