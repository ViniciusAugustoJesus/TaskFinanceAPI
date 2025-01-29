using Microsoft.AspNetCore.Mvc;
using TaskFinanceAPI.Data;
using TaskFinanceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;

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
        public ActionResult<Tarefa> PostTarefa(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = _context.Usuarios.Find(tarefa.IdUsuario);

            if (usuario == null) { return NotFound(new { Message = "Usuario não encontrado" }); }

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult PutTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarefa).State = EntityState.Modified;

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
