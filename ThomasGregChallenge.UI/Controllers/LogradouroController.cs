using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.UI.Models;
using ThomasGregChallenge.UI.Services;

namespace ThomasGregChallenge.UI.Controllers
{
    public class LogradouroController(ILogger<LogradouroController> logger,
        ClienteService clienteService,
        LogradouroService logradouroService,                  
        TokenService tokenService) : Controller
    {
        private readonly ILogger<LogradouroController> _logger = logger;
        private readonly ClienteService _clienteService = clienteService;        
        private readonly LogradouroService _logradouroService = logradouroService;
        private readonly TokenService _tokenService = tokenService;

        [HttpGet]
        public async Task<IActionResult?> Create([FromQuery] int clienteId, CancellationToken cancellationToken)
        {
            string tokenJwt = VerifyUser();
            if (string.IsNullOrWhiteSpace(tokenJwt))
                return RedirectToActionPermanent("Index", "Login");

            var clienteModel = await _clienteService.GetClientByIdAsync(clienteId, tokenJwt, cancellationToken);
            
            ViewData["Cliente"] = clienteModel.Nome;

            return View(new LogradouroModel { Bairro = "", Cidade = "", Endereco = "", Estado = "", Numero = "", ClienteId = clienteId });
        }

        [HttpGet]
        public IActionResult Index()
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
                
                _logger.LogInformation("Logradouro cadastrado com sucesso");

                return RedirectToAction("Index","Cliente");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado. {ex.Message}";
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}", ex.Message, ex.StackTrace);
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
                _logger.LogError("Algo deu errado. {Message} - {StackTrace}", ex.Message, ex.StackTrace);
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
                
                _logger.LogInformation("Logradouro atualizado com sucesso");

                return RedirectToActionPermanent("Details", "Cliente", new { id = logradouroModel.ClienteId });
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

                var result = await _logradouroService.DeleteLogradouroAsync(id, tokenJwt, cancellationToken);
                
                _logger.LogInformation("Logradouro excluído com sucesso");

                return RedirectToAction("Index","Cliente");
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

                var result = await _logradouroService.GetLogradouroByIdAsync(id, tokenJwt, cancellationToken);
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

                var result = await _logradouroService.ListarLogradourosAsync(tokenJwt, cancellationToken);
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

                var result = await _logradouroService.GetLogradouroByIdAsync(id, tokenJwt, cancellationToken);
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
