using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Lines
{
    public class IndexModel : PageModel
    {
        public IList<LineDto> Lines { get; set; }
        public IList<CityOverviewDto> Cities { get; set; }

        private ICityController _cityController;
        private ILineController _lineController;

        public IndexModel(ICityController cityController, ILineController lineController)
        {
            _cityController = cityController;
            _lineController = lineController;
        }

        public async Task OnGet()
        {
            Lines = await _lineController.GetAllLines();
            Cities = await _cityController.GetCities();
        }
    }
}