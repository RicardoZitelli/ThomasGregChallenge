using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using ThomasGregChallenge.UI.Models;
using ThomasGregChallenge.UI.Services;

namespace ThomasGregChallenge.UI.Controllers
{
    public class ClienteController(ClienteService clienteServices, LogradouroService logradouroService, TokenService tokenService) : Controller
    {
        private readonly ClienteService _clienteServicos = clienteServices;
        private readonly TokenService _tokenService = tokenService;
        private readonly LogradouroService _logradouroService = logradouroService;

        [HttpGet]
        public IActionResult Create()
        {
            string tokenJwt = VerifyUser();
            if (string.IsNullOrWhiteSpace(tokenJwt))
                return RedirectToActionPermanent("Index", "Login");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            string tokenJwt = VerifyUser();
            if (string.IsNullOrWhiteSpace(tokenJwt))
                return RedirectToActionPermanent("Index", "Login");

            var result = await _clienteServicos.ListarClientesAsync(tokenJwt, cancellationToken);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind] ClienteModel clienteModel, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _clienteServicos.SaveClienteAsync(clienteModel, tokenJwt, cancellationToken);
                TempData["Message"] = "Cliente cadastrado com sucesso";
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

                var cliente = await _clienteServicos.GetClientByIdAsync(id, tokenJwt, cancellationToken);
                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind] ClienteModel clienteModel, CancellationToken cancellationToken)
        {
            try
            {
                string tokenJwt = VerifyUser();
                if (string.IsNullOrWhiteSpace(tokenJwt))
                    return RedirectToActionPermanent("Index", "Login");

                var result = await _clienteServicos.UpdateClienteAsync(clienteModel, tokenJwt, cancellationToken);
                TempData["Message"] = "Cliente atualizado com sucesso";
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

                var result = await _clienteServicos.DeleteClienteAsync(id, tokenJwt, cancellationToken);
                TempData["Message"] = "Cliente excluído com sucesso";
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

                var result = await _clienteServicos.GetClientByIdAsync(id, tokenJwt, cancellationToken);
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

                var result = await _clienteServicos.ListarClientesAsync(tokenJwt, cancellationToken);
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

                var clienteModel = await _clienteServicos.GetClientByIdAsync(id, tokenJwt, cancellationToken);

                clienteModel.Logradouros = await _logradouroService.ObterLogradourosPorClienteAsync(clienteModel.Id, tokenJwt, cancellationToken);

                return View(clienteModel);
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