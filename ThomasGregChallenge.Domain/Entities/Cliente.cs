using System.ComponentModel.DataAnnotations;
namespace ThomasGregChallenge.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "Nome não pode ser maior do que 150 caracteres")]
        public required string Nome { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "E-mail não pode ser maior do que 150 caracteres")]
        public required string Email { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "Logotipo não pode ser maior do que 150 caracteres")]
        public required string Logotipo { get; set; }
        
        public List<Logradouro>? Logradouros { get; set; }

        public Cliente()
        {
                
        }

        public Cliente (string nome,string email, string logotipo)
        {
            Nome = nome;
            Email = email;
            Logotipo = logotipo;            
        }                
    }     
}
