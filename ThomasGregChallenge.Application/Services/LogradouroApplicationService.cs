using AutoMapper;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Application.Interfaces.Services;
using ThomasGregChallenge.Domain.Entities;
using ThomasGregChallenge.Domain.Interfaces.Services;

namespace ThomasGregChallenge.Application.Services
{
    public sealed class LogradouroApplicationService(ILogradouroService logradouroService, IMapper mapper) : ILogradouroApplicationService
    {
        private readonly ILogradouroService _logradouroService = logradouroService;
        private readonly IMapper _mapper = mapper;

        public async Task DeleteAsync(int logradouroId, CancellationToken cancellationToken)
        {
            try
            {
                var logradouro = await _logradouroService.GetByIdAsync(logradouroId, cancellationToken);

                if (logradouro is not null)
                    await _logradouroService.DeleteAsync(logradouro, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LogradouroResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var logradouros = await _logradouroService.GetAllAsync(cancellationToken);

                return _mapper.Map<IEnumerable<LogradouroResponseDto>>(logradouros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<LogradouroResponseDto>> GetByClientIdAsync(int clienteId, CancellationToken cancellationToken)
        {
            var logradouros = await _logradouroService.GetByClientIdAsync(clienteId, cancellationToken);

            return _mapper.Map<IEnumerable<LogradouroResponseDto>>(logradouros);
        }

        public async Task<IEnumerable<LogradouroResponseDto>> GetByDescriptionAsync(string description, CancellationToken cancellationToken)
        {
            try
            {
                var logradouros = await _logradouroService.GetByDescriptionAsync(description, cancellationToken);

                return _mapper.Map<IEnumerable<LogradouroResponseDto>>(logradouros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LogradouroResponseDto> GetByIdAsync(int logradouroId, CancellationToken cancellationToken)
        {
            try
            {
                var logradouro = await _logradouroService.GetByIdAsync(logradouroId, cancellationToken);

                return _mapper.Map<LogradouroResponseDto>(logradouro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SaveAsync(LogradouroRequestDto logradouroRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var logradouro = _mapper.Map<Logradouro>(logradouroRequestDto);

                await _logradouroService.AddAsync(logradouro, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task UpdateAsync(LogradouroRequestDto logradouroRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var logradouro = _mapper.Map<Logradouro>(logradouroRequestDto);

                await _logradouroService.UpdateAsync(logradouro, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }            
        }
    }
}
