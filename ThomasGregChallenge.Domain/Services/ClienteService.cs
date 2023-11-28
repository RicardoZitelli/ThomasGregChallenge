using ThomasGregChallenge.Domain.Entities;
using ThomasGregChallenge.Domain.Interfaces.Repositories;
using ThomasGregChallenge.Domain.Interfaces.Services;

namespace ThomasGregChallenge.Domain.Services
{
    public sealed class ClienteService(IClienteRepository clienteRepository) : BaseService<Cliente>(clienteRepository), IClienteService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        
        public async Task<IEnumerable<Cliente>> GetByDescriptionAsync(string description, CancellationToken cancellationToken) =>
            await _clienteRepository.GetByDescriptionAsync(description, cancellationToken);        
    }
}
