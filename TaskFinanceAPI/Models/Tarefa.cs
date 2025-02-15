using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskFinanceAPI.Models
{
    public class Tarefa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome da Tarefa deve ser informado.")]
        [StringLength(100, ErrorMessage = "O nome da Tarefa pode ter no máximo 100 caracteres ")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A descrição da Tarefa deve ser informado.")]
        [StringLength(200, ErrorMessage = "A descrição da Tarefa pode ter no máximo 200 caracteres ")]
        public string Descricao { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "O valor da Tarefa deve ser informado.")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor da Tarefa deve ser maior que zero.")]
        public double Valor { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }
    }

    public enum StatusTarefa
    {
        Pendente,
        Concluida
    }
}
