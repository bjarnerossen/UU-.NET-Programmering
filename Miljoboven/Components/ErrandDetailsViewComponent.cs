using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Components;

public class ErrandDetailsViewComponent : ViewComponent
{
    private readonly IMiljobovenRepository repository;

    public ErrandDetailsViewComponent(IMiljobovenRepository repo)
    {
        repository = repo;
    }

    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var errandDetails = await repository.GetErrandDetails(id);
        return View(errandDetails);
    }
}