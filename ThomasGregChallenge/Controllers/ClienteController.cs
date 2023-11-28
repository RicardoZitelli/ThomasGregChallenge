using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Application.Interfaces.Services;

namespace ThomasGregChallenge.Controllers
{
    /// <summary>
    /// Controller used to insert, update, retrieve and/or delete client
    /// </summary>    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController(IClienteApplicationService clienteApplicationService) : ControllerBase
    {
        private readonly IClienteApplicationService _clienteApplicationService = clienteApplicationService;

        /// <summary>
        /// Este endpoint é responsável por inserir um novo cliente ao banco de dados
        /// </summary>
        /// <param name="clienteLogradouroRequestDto"></param>
        /// <param name="cancellationToken"></param>        
        [HttpPost("Gravar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SaveAsync(ClienteLogradouroRequestDto clienteLogradouroRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                if (clienteLogradouroRequestDto is null)
                    return BadRequest("Ops, o objeto Cliente está nulo");
                                
                await _clienteApplicationService.SaveAsync(clienteLogradouroRequestDto, cancellationToken);

                return Ok("Cliente cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// Este endpoint é responsável por atualizar um cliente no banco de dados
        /// </summary>
        /// <param name="clienteRequestDto"></param>
        /// <param name="cancellationToken"></param>        
        [HttpPut("Atualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                if (clienteRequestDto is null ||
                    clienteRequestDto.Id == 0)
                    return BadRequest("Ops, nenhum cliente foi identificado");
                
                await _clienteApplicationService.UpdateAsync(clienteRequestDto!, cancellationToken);
                
                return Ok("Cliente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// Este endpoint é responsável por excluir um cliente do banco de dados
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cancellationToken"></param>        
        [HttpDelete("Excluir")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int clienteId, CancellationToken cancellationToken)
        {
            try
            {
                if (clienteId == 0)
                    return BadRequest("Ops, nenhum cliente foi identificado");

                await _clienteApplicationService.DeleteAsync(clienteId, cancellationToken);

                return Ok("Cliente excluído com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// Este endpoint é responsável por listar todos os clientes do banco de dados
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("Listar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClienteResponseDto>> ListarClientesAsync(CancellationToken cancellationToken) =>
            await _clienteApplicationService.GetAllAsync(cancellationToken);

        /// <summary>
        /// Este endpoint é responsável por obter um cliente do banco de dados
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("Obter/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ClienteResponseDto> ObterAsync([FromRoute] int id, CancellationToken cancellationToken) =>
            await _clienteApplicationService.GetByIdAsync(id, cancellationToken);

        /// <summary>
        /// Este endpoint é responsável por obter um cliente do banco de dados
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("ObterPorDescricao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClienteResponseDto>> ObterAsync([FromQuery] string description, CancellationToken cancellationToken) =>
            await _clienteApplicationService.GetByDescriptionAsync(description, cancellationToken);
    }
}
