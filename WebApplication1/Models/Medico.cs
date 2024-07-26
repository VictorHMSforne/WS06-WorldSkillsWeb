using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Medicos")]
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor Insira um nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor Insira a Hora de Entrada!")]
        [DataType(DataType.Time)]
        public TimeSpan HoraEntrada { get; set; }

        [Required(ErrorMessage = "Por favor Insira a Hora de Saída!")]
        [DataType(DataType.Time)]
        public TimeSpan HoraSaida { get; set; }

        public string Turno { get; set; }

    }
}
