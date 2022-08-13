using Infrastructure.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrainGovernor.Pages.Cities
{
    public class CreateModel : PageModel
    {
        ICityController _controller;

        public CreateModel(ICityController controller)
        {
            _controller = controller;
        }

        public void OnGet()
        {
        }
    }
}
