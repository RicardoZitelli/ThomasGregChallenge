using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Application.Interfaces.Services;

namespace ThomasGregChallenge.Controllers
{
    /// <summary>
    /// Controller utilizado para inserir, atualizar, excluir, obter e listar dados do logradouro
    /// </summary>    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogradouroController(ILogradouroApplicationService logradouroApplicationService) : ControllerBase
    {
        private readonly ILogradouroApplicationService _logradouroApplicationService = logradouroApplicationService;

        /// <summary>
        /// Este endpoint é responsável por inserir um novo logradouro ao banco de dados
        /// </summary>
        /// <param name="logradouroRequestDto"></param>
        /// <param name="validator"></param>      
        /// <param name="cancellationToken"></param>        
        [HttpPost("Gravar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SaveAsync(LogradouroRequestDto logradouroRequestDto,
            [FromServices] IValidator<LogradouroRequestDto> validator,
            CancellationToken cancellationToken)
        {
            try
            {
                if (logradouroRequestDto is null)
                    return BadRequest("Ops, o objeto Logradouro está nulo");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, logradouroRequestDto);

                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);

                await _logradouroApplicationService.SaveAsync(logradouroRequestDto, cancellationToken);

                return Ok("Logradouro cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// Este endpoint é responsável por atualizar um logradouro no banco de dados
        /// </summary>
        /// <param name="logradouroRequestDto"></param>
        /// <param name="validator"></param>        
        /// <param name="cancellationToken"></param>                
        [HttpPut("Atualizar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(LogradouroRequestDto logradouroRequestDto,
            [FromServices] IValidator<LogradouroRequestDto> validator,
            CancellationToken cancellationToken)
        {
            try
            {
                if (logradouroRequestDto is null ||
                    logradouroRequestDto.Id == 0)
                    return BadRequest("Ops, nenhum logradouro foi identificado");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, logradouroRequestDto);

                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);

                await _logradouroApplicationService.UpdateAsync(logradouroRequestDto!, cancellationToken);

                return Ok("Logradouro atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// Este endpoint é responsável por excluir um logradouro do banco de dados
        /// </summary>
        /// <param name="logradouroId"></param>
        /// <param name="cancellationToken"></param>        
        [HttpDelete("Excluir")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int logradouroId,
            CancellationToken cancellationToken)
        {
            try
            {
                if (logradouroId == 0)
                    return BadRequest("Ops, nenhum logradouro foi identificado");

                await _logradouroApplicationService.DeleteAsync(logradouroId, cancellationToken);

                return Ok("Logradouro excluído com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// Este endpoint é responsável por listar todos os logradouros do banco de dados
        /// </summary>        
        /// <param name="cancellationToken"></param>        
        [HttpGet("Listar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<LogradouroResponseDto>> ListarAsync(CancellationToken cancellationToken) =>
            await _logradouroApplicationService.GetAllAsync(cancellationToken);

        /// <summary>
        /// Este endpoint é responsável por obter um logradouro do banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("Obter/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<LogradouroResponseDto> ObterAsync([FromRoute] int id,
            CancellationToken cancellationToken) =>
            await _logradouroApplicationService.GetByIdAsync(id, cancellationToken);

        /// <summary>
        /// Este endpoint é responsável por obter um logradouro do banco de dados
        /// </summary>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("ObterPorDescricao")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<LogradouroResponseDto>> ObterAsync([FromQuery] string description,
            CancellationToken cancellationToken) =>
            await _logradouroApplicationService.GetByDescriptionAsync(description, cancellationToken);
    }
}
