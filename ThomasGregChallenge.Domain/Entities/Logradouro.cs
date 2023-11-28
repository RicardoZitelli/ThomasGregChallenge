using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThomasGregChallenge.Domain.Entities
{
    public class Logradouro
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Endereço não pode ser maior do que 100 caracteres")]
        public required string Endereco { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Numero não pode ser maior do que 20 caracteres")]
        public required string Numero { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Bairro não pode ser maior do que 100 caracteres")]
        public required string Bairro { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cidade não pode ser maior do que 100 caracteres")]
        public required string Cidade { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Estado deve possuir 2 caracteres",MinimumLength =2)]
        public required string Estado { get; set; }

        [StringLength(150, ErrorMessage = "Complemento não pode ser maior do que 150 caracteres")]
        public string? Complemento { get; set; }

        [ForeignKey("ClienteId")]
        [Required]
        public Guid ClienteId { get;  set; }

        public Logradouro()
        {
                
        }

        public Logradouro(string endereco, string numero, string bairro, string cidade, string estado, string? complemento, Guid clienteId)
        {
            Endereco = endereco;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Complemento = complemento;
            ClienteId = clienteId;            
        }
    }        
}
