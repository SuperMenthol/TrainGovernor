using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Lines
{
    public class AddModel : PageModel
    {
        public IList<TrainStationDto> Stations { get; set; }
        public IList<NeighbouringTrainStationDto> Neighbours { get; set; }

        private ILineController _lineController;
        private ITrainStationController _stationController;

        public AddModel(ILineController lineController, ITrainStationController trainStationController)
        {
            _lineController = lineController;
            _stationController = trainStationController;
        }

        public async Task OnGet()
        {
            Stations = await _stationController.GetAll();
            Neighbours = await _stationController.GetAllNeighbouringStations();
        }
    }
}
