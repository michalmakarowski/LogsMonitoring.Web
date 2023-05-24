using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using LogsMonitoring.Web.Models;

namespace LogsMonitoring.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginDbContext _dbContext;

        public AccountController(LoginDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            // Sprawdź poprawność danych logowania w bazie danych
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                    // Dodaj inne dowolne roszczenia, jeśli to konieczne
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuthenticationScheme");
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync("MyCookieAuthenticationScheme", principal).Wait();

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Nieprawidłowe dane logowania");
            return View(model);
        }
    }
}
