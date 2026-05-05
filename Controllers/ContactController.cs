using Microsoft.AspNetCore.Mvc;
using SurveProzone.Models;
using SurveProzone.Services;

namespace SurveProzone.Controllers
{
    public class ContactController : Controller
    {
        private readonly EmailService _emailService;

        // ✅ Removed AppDbContext
        public ContactController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("SERVICE VALUE: " + model.Service);

                // ❌ Removed DB save
                // _context.ContactForms.Add(model);
                // _context.SaveChanges();

                // ✅ Email still works
                _emailService.SendEmail(
                    model.Name,
                    model.Email,
                    model.Phone,
                    model.Message
                );

                ViewBag.Message = "Message Sent Successfully!";
                ModelState.Clear();
            }

            return View();
        }
    }
}