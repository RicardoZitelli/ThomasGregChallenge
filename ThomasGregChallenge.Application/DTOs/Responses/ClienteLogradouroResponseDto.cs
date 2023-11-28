namespace ThomasGregChallenge.Application.DTOs.Responses
{
    public sealed record ClienteLogradouroResponseDto(int Id, string Nome, string Email, string Logotipo, IEnumerable<LogradouroResponseDto>? Logradouros);
    
}
