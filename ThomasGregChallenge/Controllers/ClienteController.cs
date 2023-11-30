using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Application.Interfaces.Services;

namespace ThomasGregChallenge.Controllers
{
    /// <summary>
    /// Controller utilizado para inserir, atualizar, excluir, obter e listar dados do cliente
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
        /// <param name="validator"></param>
        /// <param name="cancellationToken"></param>        
        [HttpPost("Gravar")]
        [Authorize(Roles="Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SaveAsync(ClienteRequestDto clienteLogradouroRequestDto,
            [FromServices] IValidator<ClienteRequestDto> validator, 
            CancellationToken cancellationToken)
        {
            try
            {
                if (clienteLogradouroRequestDto is null)
                    return BadRequest("Ops, o objeto Cliente está nulo");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, clienteLogradouroRequestDto);
             
                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);
                                                
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
        /// <param name="validator"></param>        
        [HttpPut("Atualizar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(ClienteRequestDto clienteRequestDto,
            [FromServices] IValidator<ClienteRequestDto> validator, 
            CancellationToken cancellationToken)
        {
            try
            {
                if (clienteRequestDto is null || 
                    clienteRequestDto.Id == 0)
                    return BadRequest("Ops, nenhum cliente foi identificado");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, clienteRequestDto);

                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);

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
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>        
        [HttpDelete("Excluir")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int id, 
            CancellationToken cancellationToken)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Ops, nenhum cliente foi identificado");

                await _clienteApplicationService.DeleteAsync(id, cancellationToken);

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
        /// <param name="cancellationToken"></param>        
        [HttpGet("Listar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClienteResponseDto>> ListarAsync(CancellationToken cancellationToken) =>
            await _clienteApplicationService.GetAllAsync(cancellationToken);

        /// <summary>
        /// Este endpoint é responsável por obter um cliente do banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("Obter/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ClienteResponseDto> ObterAsync([FromRoute] int id, 
            CancellationToken cancellationToken) =>
            await _clienteApplicationService.GetByIdAsync(id, cancellationToken);

        /// <summary>
        /// Este endpoint é responsável por obter um cliente do banco de dados
        /// </summary>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("ObterPorDescricao")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClienteResponseDto>> ObterAsync([FromQuery] string description, 
            CancellationToken cancellationToken) =>
            await _clienteApplicationService.GetByDescriptionAsync(description, cancellationToken);      
    }
}
