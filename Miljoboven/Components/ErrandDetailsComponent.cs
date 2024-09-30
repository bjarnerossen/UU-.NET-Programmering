using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Components;

public class ErrandDetailsComponent : ViewComponent
{
    private readonly IMiljobovenRepository _repository;

    public ErrandDetailsComponent(IMiljobovenRepository repository)
    {
        _repository = repository;
    }

    // The Invoke method fetches the errand details by its ID from the
    // repository and returns a view component with the details.
    public IViewComponentResult Invoke(string errandId)
    {
        // Fetch the specific errand using the ID
        var errand = _repository.GetErrandById(errandId);

        if (errand == null)
        {
            // If the errand is not found, return a view component with a message
            return View("NotFound", new { Message = "Errand not found" });
        }

        // Pass the errand to the view
        return View(errand);
    }
}