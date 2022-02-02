using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Lines
{
    public class EditModel : PageModel
    {
        public LineDto Line { get; set; }
        public IList<TrainStationDto> Stations { get; set; }

        private ILineController _lineController;
        private ITrainStationController _stationController;

        public EditModel(ILineController lineController, ITrainStationController trainStationController)
        {
            _lineController = lineController;
            _stationController = trainStationController;
        }

        public async Task OnGet(int lineId)
        {
            Line = _lineController.GetLine(lineId);
            Stations = await _stationController.GetAll();
        }
    }
}