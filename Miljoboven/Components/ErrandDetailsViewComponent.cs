using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Components;

public class ErrandDetailsViewComponent : ViewComponent
{
    private readonly IMiljobovenRepository _repository;

    public ErrandDetailsViewComponent(IMiljobovenRepository repository)
    {
        _repository = repository;
    }

    public async Task<IViewComponentResult> InvokeAsync(string id)
    {
        var errandDetails = await _repository.GetErrandDetails(id);
        return View(errandDetails);
    }
}