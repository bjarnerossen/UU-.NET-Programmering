using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    public class InvestigatorController : Controller
    {
        private readonly IMiljobovenRepository _repository;

        public InvestigatorController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult StartInvestigator()
        {
            return View(_repository);
        }

        public ViewResult CrimeInvestigator(int id)
        {
            ViewBag.ID = id;
            return View(_repository);
        }
    }
}
