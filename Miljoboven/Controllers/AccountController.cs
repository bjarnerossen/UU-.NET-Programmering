using Microsoft.AspNetCore.Mvc;

namespace Miljoboven.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        public ViewResult Login()
        {
            return View();
        }

    }
}
