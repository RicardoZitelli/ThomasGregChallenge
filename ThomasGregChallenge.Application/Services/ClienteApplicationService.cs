using AutoMapper;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Application.Interfaces.Services;
using ThomasGregChallenge.Domain.Entities;
using ThomasGregChallenge.Domain.Interfaces.Services;

namespace ThomasGregChallenge.Application.Services
{
    public sealed class ClienteApplicationService(IClienteService clienteService,
        IMapper mapper) : IClienteApplicationService
    {
        private readonly IClienteService _clienteService = clienteService;
        private readonly IMapper _mapper = mapper;

        public async Task DeleteAsync(int clienteId, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _clienteService.GetByIdAsync(clienteId, cancellationToken);

                if (cliente is not null)
                    await _clienteService.DeleteAsync(cliente, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ClienteResponseDto> GetByIdAsync(int clienteId, CancellationToken cancellationToken)
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

        public async Task SaveAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken)
        {
            try
            {                
                if (await EmailJaExiste(clienteRequestDto.Email, cancellationToken))
                    throw new Exception("Cliente já existe na base de dados");

                var cliente = _mapper.Map<Cliente>(clienteRequestDto);

                await _clienteService.AddAsync(cliente, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(ClienteRequestDto clienteRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                if (await EmailJaExiste(clienteRequestDto.Email, cancellationToken))
                    throw new Exception("Este email já está em uso");

                var cliente = _mapper.Map<Cliente>(clienteRequestDto);

                await _clienteService.UpdateAsync(cliente, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ClienteResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var clientes = await _clienteService.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
        }

        private async Task<bool> EmailJaExiste(string email, CancellationToken cancellationToken)
        {
            var emailCliente = await _clienteService.GetByDescriptionAsync(email, cancellationToken);

            if (emailCliente.Any(x => x.Email == email))
                return true;

            return false;
        }
    }
}
