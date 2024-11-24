using Microsoft.AspNetCore.Mvc;
using Lab_5.Library;
using Microsoft.AspNetCore.Authorization;

namespace Lab_5.WebApp.Controllers
{
    public class Lab2Controller : Controller
    {
        private readonly LabRunner _runner;

        public Lab2Controller()
        {
            _runner = new LabRunner();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string input, string output)
        {
            _runner.RunLab_2(input, output);
            ViewBag.Result = System.IO.File.ReadAllText(output);
            return View();
        }
    }
}
