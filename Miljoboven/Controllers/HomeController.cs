using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMiljobovenRepository repository;

        public HomeController(IMiljobovenRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            var newErrand = HttpContext.Session.GetJson<Errand>("CitizenNewErrand");
            if (newErrand == null)
            {
                return View();
            }
            else
            {
                return View(newErrand);
            }
        }

        public ViewResult Login()
        {
            return View();
        }
    }
}
