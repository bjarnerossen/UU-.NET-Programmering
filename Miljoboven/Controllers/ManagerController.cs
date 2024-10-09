using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    public class ManagerController : Controller
    {
        // Inject the repository
        private readonly IMiljobovenRepository _repository;

        public ManagerController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult StartManager()
        {
            return View(_repository);
        }


        public ViewResult CrimeManager(string id)
        {
            ViewBag.ID = id;
            return View(_repository);
        }
    }
}
