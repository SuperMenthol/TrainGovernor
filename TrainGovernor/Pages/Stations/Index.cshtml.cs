using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Stations
{
    public class IndexModel : PageModel
    {
        public IList<TrainStationDto> Stations { get; set; }
        public IList<CityOverviewDto> Cities { get; set; }

        ICityController _cityController;
        ITrainStationController _stationController;
        
        public IndexModel(ITrainStationController stationController, ICityController cityController)
        {
            _cityController = cityController;
            _stationController = stationController;
        }

        public async Task OnGetAsync()
        {
            Cities = await _cityController.GetCities();
            Stations = await _stationController.GetAll();
        }
    }
}