using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

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
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }
    }
}
