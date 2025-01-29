using Microsoft.AspNetCore.Mvc;
using TaskFinanceAPI.Data;
using TaskFinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

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

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<Usuario> PostUser(Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

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

