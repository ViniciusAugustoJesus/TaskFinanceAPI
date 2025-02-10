using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFinanceAPI.Data;
using TaskFinanceAPI.DTOs;
using TaskFinanceAPI.Models;

namespace TaskFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context) { _context = context; }


        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsers()
        {
            return _context.Usuarios.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUser(int id)
        {
            var user = _context.Usuarios.Find(id);
            user.Tarefas = _context.Tarefas.Where(t => t.IdUsuario == id).ToList();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<Usuario> PostUser([FromBody] UsuarioDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Usuario
            {
                Nome = userDTO.Nome,
                Saldo = userDTO.Saldo,
            };

            _context.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] UsuarioDTO userDTO)
        {

            if (userDTO == null)
            {
                return BadRequest();
            }

            var userExistente = _context.Usuarios.Find(id);

            if (userExistente == null)
            {
                return NotFound();
            }

            userExistente.Nome = userDTO.Nome;
            userExistente.Saldo = userDTO.Saldo;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Usuarios.Find(id);

            if (user == null)
            {
                return NoContent();
            }

            _context.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }


    }
}

