using Microsoft.AspNetCore.Mvc;
using Lab_5.WebApp.Models;

namespace Lab_5.WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Please fill in all fields.";
            return View();
        }

        public IActionResult Profile()
        {
            // Повертає профіль користувача
            return View();
        }
    }
}
