using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.UI.Models;
using ThomasGregChallenge.UI.Services;

namespace ThomasGregChallenge.UI.Controllers
{
    public class ClienteController(ILogger<ClienteController> logger, 
        ClienteService clienteService, 
        LogradouroService logradouroService, 
        TokenService tokenService) : Controller
    {
        private readonly ILogger<ClienteController> _logger = logger;
        private readonly ClienteService _clienteService = clienteService;
        private readonly LogradouroService _logradouroService = logradouroService;
        private readonly TokenService _tokenService = tokenService;

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

            var result = await _clienteService.ListarClientesAsync(tokenJwt, cancellationToken);
            
            _logger.LogInformation("Retorno de lista de clientes realizado com sucesso");

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

                var result = await _clienteService.SaveClienteAsync(clienteModel, tokenJwt, cancellationToken);
                
                _logger.LogInformation("Cliente cadastrado com sucesso");
                
                return RedirectToAction("Index","Cliente");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                return RedirectToAction("Index","Cliente");
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

                var cliente = await _clienteService.GetClientByIdAsync(id, tokenJwt, cancellationToken);
                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}",ex.Message,ex.StackTrace);
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

                var result = await _clienteService.UpdateClienteAsync(clienteModel, tokenJwt, cancellationToken);
                
                _logger.LogInformation("Cliente atualizado com sucesso");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}", ex.Message, ex.StackTrace);
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

                var result = await _clienteService.DeleteClienteAsync(id, tokenJwt, cancellationToken);
                
                _logger.LogInformation("Cliente excluído com sucesso");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}", ex.Message, ex.StackTrace);
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

                var result = await _clienteService.GetClientByIdAsync(id, tokenJwt, cancellationToken);
                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}", ex.Message, ex.StackTrace);
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

                var result = await _clienteService.ListarClientesAsync(tokenJwt, cancellationToken);
                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}", ex.Message, ex.StackTrace);
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

                var clienteModel = await _clienteService.GetClientByIdAsync(id, tokenJwt, cancellationToken);

                clienteModel.Logradouros = await _logradouroService.ObterLogradourosPorClienteAsync(clienteModel.Id, tokenJwt, cancellationToken);

                return View(clienteModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}", ex.Message, ex.StackTrace);
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