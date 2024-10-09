using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    public class CitizenController : Controller
    {
        private readonly IMiljobovenRepository _repository;

        public CitizenController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Services()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }

        public ViewResult Faq()
        {
            return View();
        }

        public ViewResult Thanks()
        {
            return View();
        }
    }
}
