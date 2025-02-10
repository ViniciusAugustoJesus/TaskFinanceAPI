using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFinanceAPI.Data;
using TaskFinanceAPI.DTOs;
using TaskFinanceAPI.Models;

namespace TaskFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tarefa>> GetTarefas()
        {
            return _context.Tarefas.Include(t => t.Usuario).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tarefa> GetTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;

        }

        [HttpPost]
        public ActionResult<Tarefa> PostTarefa([FromBody] TarefaDTO tarefaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = _context.Usuarios.Find(tarefaDto.IdUsuario);

            if (usuario == null) { return NotFound(new { Message = "Usuario não encontrado" }); }

            var tarefa = new Tarefa
            {
                Nome = tarefaDto.Nome,
                Descricao = tarefaDto.Descricao,
                Valor = tarefaDto.Valor,
                Concluida = tarefaDto.Concluida,
                IdUsuario = tarefaDto.IdUsuario,
            };

            _context.Tarefas.Add(tarefa);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao salvar a tarefa", Detalhes = ex.Message });
            }


            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult PutTarefa(int id, [FromBody] TarefaDTO tarefaDto)
        {

            if (tarefaDto == null)
            {
                return BadRequest();
            }

            var tarefaExistente = _context.Tarefas.Find(id);

            if (tarefaExistente == null)
            {
                return NotFound();
            }

            tarefaExistente.Nome = tarefaDto.Nome;
            tarefaExistente.Descricao = tarefaDto.Descricao;
            tarefaExistente.Valor = tarefaDto.Valor;
            tarefaExistente.Concluida = tarefaDto.Concluida;
            tarefaExistente.IdUsuario = tarefaDto.IdUsuario;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tarefas.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();

        }

    }

}
