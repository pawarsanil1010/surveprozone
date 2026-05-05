using Microsoft.AspNetCore.Mvc;

namespace SurveProzone.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CCTV()
        {
            return View();
        }

        public IActionResult Biometric()
        {
            return View();
        }

        public IActionResult FireSafety()
        {
            return View();
        }

        public IActionResult Automation()
        {
            return View();
        }
    }
}