using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Extensions;

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
            ViewBag.Title = "Bekräfta - Medlem";

            if (ModelState.IsValid) // Kontrollera att modellen är giltig
            {
                HttpContext.Session.SetJson("NewErrand", errand); 
                return View("Validate", errand); // Returnera Validate-vyn
            }

            // Om modellen inte är giltig, gå tillbaka till formuläret med felmeddelanden
            return View("../Home/Index", errand); // Returnera till Index-viewn och visa felmedellanden 
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
