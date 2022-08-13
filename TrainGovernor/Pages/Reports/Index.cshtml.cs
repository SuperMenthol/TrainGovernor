using Infrastructure.Interfaces.Controllers;
using Infrastructure.Models.Dto;
using Infrastructure.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Reports
{
    public class IndexModel : PageModel
    {
        public List<LineDto> Lines { get; set; }

        private ILineController _lineController;
        private ILineStartTimeController _lineStartTimeController;

        public IndexModel(ILineStartTimeController controller, ILineController lineController)
        {
            _lineStartTimeController = controller;
            _lineController = lineController;
        }

        public void OnGet()
        {
            Lines = _lineController.GetLinesHavingStartTimes();
        }
    }
}