using Microsoft.AspNetCore.Mvc;
using SurveProzone.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Org.BouncyCastle.Crypto.Generators;

namespace SurveProzone.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Login Page
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        // POST: Login Action (UPDATED - DB LOGIN)
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.AdminUsers
                .FirstOrDefault(x => x.Username == model.Username);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                HttpContext.Session.SetString("User", user.Username);
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Error = "Invalid Username or Password!";
            return View(model);
        }

       

        // TEMP: Create Admin (RUN ONCE ONLY)

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}