using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seguros_ABC.Context;
using Seguros_ABC.Models;

namespace Seguros_ABC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AseguradoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Asegurado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asegurado>>> GetAsegurados()
        {
            var asegurados = await _context.Asegurados.ToListAsync();
            if (!asegurados.Any())
            {
                return NotFound(new { message = "No hay registros en la BD." });
            }
            return asegurados;
        }

        // GET: api/Asegurado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asegurado>> GetAsegurado(int id)
        {
            var asegurado = await _context.Asegurados.FindAsync(id);

            if (asegurado == null)
            {
                return NotFound(new { message = $"Asegurado con ID {id} no encontrado." });
            }

            return asegurado;
        }

        // PUT: api/Asegurado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsegurado(int id, Asegurado asegurado)
        {
            if (id != asegurado.NumeroIdentificacion)
            {
                return BadRequest();
            }

            _context.Entry(asegurado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AseguradoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Asegurado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asegurado>> PostAsegurado(Asegurado asegurado)
        {
            _context.Asegurados.Add(asegurado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsegurado", new { id = asegurado.NumeroIdentificacion }, asegurado);
        }

        // DELETE: api/Asegurado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsegurado(int id)
        {
            var asegurado = await _context.Asegurados.FindAsync(id);
            if (asegurado == null)
            {
                return NotFound(new { message = $"Asegurado con ID {id} no encontrado." });
            }

            _context.Asegurados.Remove(asegurado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AseguradoExists(int id)
        {
            return _context.Asegurados.Any(e => e.NumeroIdentificacion == id);
        }
    }
}
