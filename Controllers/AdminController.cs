using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveProzone.Models;

namespace SurveProzone.Controllers
{
    public class AdminController : Controller
    {
        // ✅ TEMP: No database

        public IActionResult EditContent()
        {
            return View(); // no DB
        }

        [HttpPost]
        public IActionResult EditContent(SiteContent model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.Message = "Updated Successfully! (Temp mode)";
            return View(model);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                context.Result = RedirectToAction("Login", "Account");
            }

            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("User");

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // ✅ TEMP: No DB data
            ViewBag.Total = 0;
            ViewBag.Today = 0;
            ViewBag.Month = 0;

            ViewBag.ServiceLabels = new List<string>();
            ViewBag.ServiceCounts = new List<int>();

            return View(); // no model
        }
    }
}