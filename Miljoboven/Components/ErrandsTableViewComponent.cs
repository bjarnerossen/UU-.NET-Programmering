using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Components;
public class ErrandsTableViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string message, IEnumerable<MyErrand> errandList, string controller, string action)
    {
        var viewModel = new ErrandTableViewModel
        {
            Message = message,
            ErrandList = errandList,
            Controller = controller,
            Action = action
        };

        return View(viewModel);
    }
}
