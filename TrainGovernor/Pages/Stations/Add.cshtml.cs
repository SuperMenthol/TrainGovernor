using Infrastructure.Interfaces.Controllers;
using Infrastructure.Models.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Stations
{
    public class AddModel : PageModel
    {
        public IList<CityOverviewDto> Cities { get; set; }
        public TrainStationDto TrainStationDto { get; set; }

        ICityController _cityController;
        ITrainStationController _stationController;

        public AddModel(ICityController cityController, ITrainStationController trainStationController)
        {
            _cityController = cityController;
            _stationController = trainStationController;

            TrainStationDto = new TrainStationDto();
        }

        public async Task OnGet()
        {
            Cities = await _cityController.GetCities();
        }

        public void OnPost()
        {
            _stationController.AddStation(TrainStationDto);
        }
    }
}
