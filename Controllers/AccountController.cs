using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PMSAT.Models;

namespace PMSAT.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}
