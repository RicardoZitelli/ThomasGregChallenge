using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Text;
using ThomasGregChallenge.UI.Models;
using ThomasGregChallenge.UI.Services;

namespace ThomasGregChallenge.UI.Controllers
{
    public class HomeController(ILogger<HomeController> logger, TokenService tokenService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly TokenService _tokenService = tokenService;

        public IActionResult Index()
        {
            var token = VerifyUser();
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToActionPermanent("Index", "Login");

            return View();
        }

        public IActionResult Privacy()
        {
            var token = VerifyUser();
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToActionPermanent("Index", "Login");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string VerifyUser()
        {
            var tokenJwt = Request.Cookies["jwtToken"]?.ToString() ?? string.Empty;
            if (_tokenService.TokenExists(tokenJwt))
                return tokenJwt;

            return string.Empty;
        }
    }
}
