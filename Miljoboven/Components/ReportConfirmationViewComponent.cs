using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Components;

public class ReportConfirmationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Errand errand)
    {
        return View(errand);
    }
}