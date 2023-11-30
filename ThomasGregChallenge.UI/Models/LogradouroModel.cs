using System.ComponentModel.DataAnnotations;

namespace ThomasGregChallenge.UI.Models
{
    public class LogradouroModel
    {
        public int Id { get; set; }

        [StringLength(150)]
        [Display(Name="Endereço")]
        public required string Endereco { get; set; }

        [StringLength(20)]
        public required string Numero { get; set; }

        [StringLength(100)]
        public required string Bairro { get; set; }

        [StringLength(100)]
        public required string Cidade { get; set; }

        [StringLength(2)]
        public required string Estado { get; set;}

        [StringLength(150)]
        public string? Complemento { get; set; }

        public int ClienteId { get; set; }

    }
}
