using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP3.Domain.Entities
{
    [Table("tb_")]
    public class BarcoEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do barco é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O modelo do barco é obrigatório.")]
        public string Modelo { get; set; }

        public int Ano { get; set; }

        public double Tamanho { get; set; }
    }
}
