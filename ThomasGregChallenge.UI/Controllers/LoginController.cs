using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.UI.Models;
using ThomasGregChallenge.UI.Services;

namespace ThomasGregChallenge.UI.Controllers
{
    public class LoginController(TokenService tokenService) : Controller
    {
        private readonly TokenService _tokenService = tokenService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> Index([Bind]UsuarioModel usuarioModel, CancellationToken cancellationToken)
        {
            var token = await _tokenService.AutenticateAsync(usuarioModel, cancellationToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(2),
            };

            Response.Cookies.Append("jwtToken", token, cookieOptions);
            return RedirectToActionPermanent("Index","Home");
        }
    }
}
