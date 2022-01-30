using Domain.Interfaces.Controllers;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Cities
{
    public class EditModel : PageModel
    {
        ICityController _cityController;
        ITrainStationController _stationController;
        public CityOverviewDto City { get; private set; }

        public EditModel(ICityController cityController, ITrainStationController stationController)
        {
            _cityController = cityController;
            _stationController = stationController;
        }

        public async Task OnGet([FromRoute] int cityid)
        {
            City = await _cityController.GetById(cityid);
        }
    }
}