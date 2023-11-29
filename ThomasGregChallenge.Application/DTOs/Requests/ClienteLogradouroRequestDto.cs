namespace ThomasGregChallenge.Application.DTOs.Requests
{
    public sealed record ClienteLogradouroRequestDto(int Id, string Nome, string Email, string Logotipo, IEnumerable<LogradouroRequestDto>? Logradouros);
}
