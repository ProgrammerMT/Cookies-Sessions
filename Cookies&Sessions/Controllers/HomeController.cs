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

        // ����� �� �������� �� userName � �������
        [HttpPost]
        public IActionResult SetUserName(string userName)
        {
            HttpContext.Session.SetString("UserName", userName);
            return RedirectToAction("Index");
        }

        // ����� �� �������� �� ������������� �� ���� � ���������
        public ActionResult SetCookie(string preference)
        {
            Response.Cookies.Append("UserPreference", preference, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7), // ����������� ������ ���� 7 ���
                HttpOnly = true,                          // ���� �������� ��� ������ �� �����������
                IsEssential = true
            });
            return RedirectToAction("Index");
        }

        // ����� �� ��������� �� ��������� ��������
        public IActionResult Index()
        {
            // �������� �� ��������� � ������������� �������������
            if (Request.Cookies.TryGetValue("UserPreference", out string userPreference))
            {
                ViewBag.UserPreference = userPreference;
            }
            else
            {
                ViewBag.UserPreference = "Default";  // �������� �� ������������
            }

            // �������� �� ������������� ��� � �������
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
