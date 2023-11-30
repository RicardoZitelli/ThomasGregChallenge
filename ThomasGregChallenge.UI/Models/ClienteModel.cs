using System.ComponentModel.DataAnnotations;

namespace ThomasGregChallenge.UI.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [StringLength(150)]
        public required string Nome { get; set; }

        [StringLength(150)]
        public required string Email { get; set; }

        [StringLength(150)]
        public required string Logotipo { get; set; }
        
        public IEnumerable<LogradouroModel>? Logradouros { get; set; }
    }
}