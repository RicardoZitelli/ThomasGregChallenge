using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.Interfaces.Services;

namespace ThomasGregChallenge.Controllers
{
    /// <summary>
    /// Controller used to insert, update, retrieve and/or delete client
    /// </summary>    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController(IClienteApplicationService applicationService) : ControllerBase
    {
        private readonly IClienteApplicationService _applicationService= applicationService;

        /// <summary>
        /// Este endpoint é responsável por inserir um novo cliente ao banco de dados
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>        
        [HttpPost("GravarCliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SaveClientAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken)
        {
            try
            {                
                if (clienteRequestDto is null)
                    BadRequest("Ops, nenhum cliente foi identificado");
                            
                await _applicationService.SaveAsync(clienteRequestDto!, cancellationToken);

                return Ok("Cliente cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }
    }
}
