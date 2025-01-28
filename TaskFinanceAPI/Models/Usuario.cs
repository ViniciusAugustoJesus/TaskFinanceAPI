using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace TaskFinanceAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do usuario deve ser informado.")]
        [StringLength(100, ErrorMessage = "O nome do usuario pode ter no máximo 100 caracteres ")]
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}
