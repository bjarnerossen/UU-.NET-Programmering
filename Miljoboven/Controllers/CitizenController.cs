using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    public class CitizenController : Controller
    {
        private readonly IMiljobovenRepository _repository;

        public CitizenController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("CitizenNewErrand", errand);
            return View(errand);

        }

        public ViewResult Thanks()
        {
            var errand = HttpContext.Session.GetJson<Errand>("CitizenNewErrand");
            _repository.SaveErrand(errand);
            ViewBag.RefNumber = errand.RefNumber;
            HttpContext.Session.Remove("CitizenNewErrand");
            return View(errand);
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
    }
}
