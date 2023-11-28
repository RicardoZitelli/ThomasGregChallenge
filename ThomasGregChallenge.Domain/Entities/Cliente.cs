using System.ComponentModel.DataAnnotations;
namespace ThomasGregChallenge.Domain.Entities
{
    public sealed class Cliente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "Nome não pode ser maior do que 150 caracteres")]
        public required string Nome { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "E-mail não pode ser maior do que 150 caracteres")]
        public required string Email { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "Logotipo não pode ser maior do que 150 caracteres")]
        public required string Logotipo { get; set; }
        
        public IEnumerable<Logradouro>? Logradouros { get; set; }

        public Cliente()
        {
                
        }

        public Cliente (int id, string nome,string email, string logotipo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Logotipo = logotipo;            
        }                
    }     
}
