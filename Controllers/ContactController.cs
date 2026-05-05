using Microsoft.AspNetCore.Mvc;
using SurveProzone.Models;
using SurveProzone.Services;

namespace SurveProzone.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public ContactController(AppDbContext context, EmailService emailService)
        {
            _context = context;
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
                _context.ContactForms.Add(model);
                _context.SaveChanges();

                // 🔥 SEND EMAIL HERE
                _emailService.SendEmail(model.Name, model.Email, model.Phone, model.Message);

                ViewBag.Message = "Message Sent Successfully!";
                ModelState.Clear();
            }

            return View();
        }
    }
}