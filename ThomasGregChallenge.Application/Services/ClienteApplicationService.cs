using AutoMapper;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Application.Interfaces.Services;
using ThomasGregChallenge.Domain.Entities;
using ThomasGregChallenge.Domain.Interfaces.Services;

namespace ThomasGregChallenge.Application.Services
{
    public class ClienteApplicationService(IClienteService clienteService, IMapper mapper) : IClienteApplicationService
    {
        private readonly IClienteService _clienteService = clienteService;
        private readonly IMapper _mapper = mapper;

        public async Task DeleteAsync(Guid clienteId, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _clienteService.GetByIdAsync(clienteId,cancellationToken);
                
                if(cliente is not null)
                    await _clienteService.DeleteAsync(cliente, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ClienteResponseDto> GetByIdAsync(Guid clienteId, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _clienteService.GetByIdAsync(clienteId, cancellationToken);

                return _mapper.Map<ClienteResponseDto>(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ClienteResponseDto>> GetByDescriptionAsync(string description, CancellationToken cancellationToken)
        {
            try
            {
                var clientes = await _clienteService.GetByDescriptionAsync(description, cancellationToken);

                return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public Task SaveAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteRequestDto);

                _clienteService.AddAsync(cliente, cancellationToken);

                return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }           
        }

        public Task UpdateAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteRequestDto);

                _clienteService.AddAsync(cliente, cancellationToken);

                return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }            
        }
    }
}
