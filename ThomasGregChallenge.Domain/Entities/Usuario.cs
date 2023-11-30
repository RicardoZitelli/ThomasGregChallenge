namespace ThomasGregChallenge.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Senha { get; set; }
        public string? Funcao { get; set; }        
    }
}
