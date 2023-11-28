namespace ThomasGregChallenge.Application.DTOs.Requests
{
    public sealed record LogradouroRequestDto(int Id, string Endereco, string Numero, string Bairro, string Cidade, string Estado, string Complemento, int ClienteId);    
}
