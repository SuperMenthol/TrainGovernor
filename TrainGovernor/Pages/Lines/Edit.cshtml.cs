using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Lines
{
    public class EditModel : PageModel
    {
        public LineDto Line { get; set; }
        public IList<TrainStationDto> Stations { get; set; }
        public IList<NeighbouringTrainStationDto> Neighbours { get; set; }
        public IList<LineStartTimeDto> StartTimes { get; set; }

        private ILineController _lineController;
        private ITrainStationController _stationController;
        private ILineStartTimeController _lineStartTimeController;

        public EditModel(ILineController lineController, ITrainStationController trainStationController, ILineStartTimeController startTimeController)
        {
            _lineController = lineController;
            _stationController = trainStationController;
            _lineStartTimeController = startTimeController;
        }

        public async Task OnGet(int lineId)
        {
            Line = _lineController.GetLine(lineId);
            Stations = await _stationController.GetAll();
            Neighbours = await _stationController.GetAllNeighbouringStations();
            StartTimes = await _lineStartTimeController.GetLineStartTimesForLine(lineId, false);
        }
    }
}