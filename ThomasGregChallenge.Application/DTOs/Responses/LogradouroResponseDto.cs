namespace ThomasGregChallenge.Application.DTOs.Responses
{
    public sealed record LogradouroResponseDto(int Id, string Endereco, string Numero,string Bairro, string Estado, string? Complemento, int ClienteId);
}
