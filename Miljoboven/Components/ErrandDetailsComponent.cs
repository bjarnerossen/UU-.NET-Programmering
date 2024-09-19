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
    public IViewComponentResult Invoke(int errandId)
    {
        // Fetch the specific errand using the ID
        var errand = _repository.GetErrandById(errandId);

        // Pass the errand model to the view
        return View(errand);
    }
}