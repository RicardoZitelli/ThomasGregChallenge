using ThomasGregChallenge.Domain.Entities;
using ThomasGregChallenge.Domain.Interfaces.Repositories;
using ThomasGregChallenge.Domain.Interfaces.Services;

namespace ThomasGregChallenge.Domain.Services
{
    public sealed class LogradouroService(ILogradouroRepository logradouroRepository) : BaseService<Logradouro>(logradouroRepository), ILogradouroService
    {
        private readonly ILogradouroRepository _logradouroRepository = logradouroRepository;
        public async Task<IEnumerable<Logradouro>> GetByDescriptionAsync(string description, CancellationToken cancellationToken) => 
            await _logradouroRepository.GetByDescriptionAsync(description, cancellationToken);

    }
}
