using Cookies_Sessions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cookies_Sessions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Метод за задаване на userName в сесията
        [HttpPost]
        public IActionResult SetUserName(string userName)
        {
            HttpContext.Session.SetString("UserName", userName);
            return RedirectToAction("Index");
        }

        // Метод за задаване на предпочитание на тема в бисквитка
        public ActionResult SetCookie(string preference)
        {
            Response.Cookies.Append("UserPreference", preference, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7), // Бисквитката изтича след 7 дни
                HttpOnly = true,                          // Само сървърът има достъп до бисквитката
                IsEssential = true
            });
            return RedirectToAction("Index");
        }

        // Метод за показване на началната страница
        public IActionResult Index()
        {
            // Проверка за бисквитка с потребителско предпочитание
            if (Request.Cookies.TryGetValue("UserPreference", out string userPreference))
            {
                ViewBag.UserPreference = userPreference;
            }
            else
            {
                ViewBag.UserPreference = "Default";  // Стойност по подразбиране
            }

            // Проверка за потребителско име в сесията
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
