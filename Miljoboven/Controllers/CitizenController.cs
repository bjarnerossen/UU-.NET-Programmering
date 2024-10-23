using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    public class CitizenController : Controller
    {
        private readonly IMiljobovenRepository repository;

        public CitizenController(IMiljobovenRepository repo)
        {
            repository = repo;
        }

        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("CitizenNewErrand", errand);
            ViewBag.errand = errand;
            return View(errand);

        }

        public ViewResult Thanks()
        {
            var errand = HttpContext.Session.GetJson<Errand>("CitizenNewErrand");
            repository.SaveErrand(errand);
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
