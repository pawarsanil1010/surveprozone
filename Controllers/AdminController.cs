using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveProzone.Models;
using System.Linq;

namespace SurveProzone.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult EditContent()
        {
            var content = _context.SiteContents.FirstOrDefault();

            if (content == null)
            {
                content = new SiteContent
                {
                    HeroTitle = "Welcome to SurveProzone",
                    HeroSubtitle = "Your Safety, Our Priority",
                    ButtonText = "Get Quote" // ✅ IMPORTANT
                };

                _context.SiteContents.Add(content);
                _context.SaveChanges();
            }

            return View(content);
        }

        [HttpPost]
        public IActionResult EditContent(SiteContent model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // ❌ stops empty submit
            }

            var content = _context.SiteContents.FirstOrDefault();

            if (content != null)
            {
                content.HeroTitle = model.HeroTitle;
                content.HeroSubtitle = model.HeroSubtitle;
                content.ButtonText = model.ButtonText;

                _context.SaveChanges();
            }

            ViewBag.Message = "Updated Successfully!";
            return View(content);
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

            var data = _context.ContactForms.ToList();

            // Dashboard counts
            ViewBag.Total = data.Count;
            ViewBag.Today = data.Count(x => x.CreatedAt.Date == DateTime.Today);
            ViewBag.Month = data.Count(x => x.CreatedAt.Month == DateTime.Now.Month);

            // 🔥 PIE CHART FIX (IMPORTANT)
            var serviceData = data
                .Where(x => !string.IsNullOrEmpty(x.Service))
                .GroupBy(x => x.Service.Trim())
                .Select(g => new
                {
                    Service = g.Key,
                    Count = g.Count()
                })
                .ToList();

            ViewBag.ServiceLabels = serviceData.Select(x => x.Service).ToList();
            ViewBag.ServiceCounts = serviceData.Select(x => x.Count).ToList();

            return View(data);
        }
    }
}