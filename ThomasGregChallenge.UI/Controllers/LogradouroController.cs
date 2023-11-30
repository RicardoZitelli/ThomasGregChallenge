using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.UI.Models;
using ThomasGregChallenge.UI.Services;

namespace ThomasGregChallenge.UI.Controllers
{
    public class LogradouroController(LogradouroService logradouroService, TokenService tokenService) : Controller
    {
        private readonly LogradouroService _logradouroService = logradouroService;
        private readonly TokenService _tokenService = tokenService;

        [HttpGet]
        public IActionResult Create([FromQuery] int clienteId)
        {
            string tokenJwt = VerifyUser();
            if (string.IsNullOrWhiteSpace(tokenJwt))
                return RedirectToActionPermanent("Index", "Login");

            return View(new LogradouroModel { Bairro = "", Cidade = "", Endereco = "", Estado = "", Numero = "", ClienteId = clienteId });
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Create(LogradouroModel logradouroModel, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");
                               
                var result = await _logradouroService.SaveLogradouroAsync(logradouroModel, tokenJwt, cancellationToken);
                TempData["Message"] = "Logradouro cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var cliente = await _logradouroService.GetLogradouroByIdAsync(id, tokenJwt, cancellationToken);
                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind] LogradouroModel logradouroModel, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _logradouroService.UpdateLogradouroAsync(logradouroModel, tokenJwt, cancellationToken);
                TempData["Message"] = "Logradouro atualizado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deletar([Bind] int id, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _logradouroService.DeleteLogradouroAsync(id, tokenJwt, cancellationToken);
                TempData["Message"] = "Logradouro excluído com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _logradouroService.GetLogradouroByIdAsync(id, tokenJwt, cancellationToken);
                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync(CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _logradouroService.ListarLogradourosAsync(tokenJwt, cancellationToken);
                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _logradouroService.GetLogradouroByIdAsync(id, tokenJwt, cancellationToken);
                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarPorClienteAsync(int clienteId, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _logradouroService.ObterLogradourosPorClienteAsync(clienteId, tokenJwt, cancellationToken);
                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
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
