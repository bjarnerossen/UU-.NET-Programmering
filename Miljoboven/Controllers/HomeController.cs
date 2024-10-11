using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMiljobovenRepository _repository;

        public HomeController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index()
        {
            var newErrand = HttpContext.Session.GetJson<Errand>("NewErrand");
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
