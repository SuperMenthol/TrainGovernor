using Infrastructure.Interfaces.Controllers;
using Infrastructure.Models.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Cities
{
    public class IndexModel : PageModel
    {
        ICityController _controller;
        public IndexModel(ICityController controller)
        {
            _controller = controller;
        }
        public async Task OnGetAsync()
        {
            Cities = await _controller.GetCities();
        }

        public IList<CityOverviewDto> Cities { get; set; }
    }
}
