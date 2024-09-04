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
        public async Task<ActionResult<IEnumerable<Asegurado>>> GetAsegurados(
            [FromQuery] string sortBy = "PrimerNombre",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest("La página y el tamaño de página deben ser mayores a cero.");
                }

                var asegurados = await _context.Asegurados
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (!asegurados.Any())
                {
                    return NotFound(new { message = "No hay registros en la BD." });
                }

                return asegurados;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                return StatusCode(500, new { message = $"Ocurrió un error al listar los asegurados. Por favor, intente nuevamente más tarde. {ex.Message}" });
            }
        }

        // GET: api/Asegurado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asegurado>> GetAsegurado(int id)
        {
            try
            {
                var asegurado = await _context.Asegurados.FindAsync(id);

                if (asegurado == null)
                {
                    return NotFound(new { message = $"Asegurado con ID {id} no encontrado." });
                }

                return asegurado;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                return StatusCode(500, new { message = $"Ocurrió un error al listar el asegurado. Por favor, intente nuevamente más tarde. {ex.Message}" });
            }
        }

        // PUT: api/Asegurado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsegurado(int id, Asegurado asegurado)
        {
            try
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
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                return StatusCode(500, new { message = $"Ocurrió un error al modificar el asegurado. Por favor, intente nuevamente más tarde. {ex.Message}" });
            }
        }

        // POST: api/Asegurado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asegurado>> PostAsegurado(Asegurado asegurado)
        {
            try
            {
                // Verificar si el asegurado ya existe en la BD
                var aseguradoExistente = await _context.Asegurados.FirstOrDefaultAsync(a => a.NumeroIdentificacion == asegurado.NumeroIdentificacion);

                if (aseguradoExistente != null)
                {
                    return BadRequest(new { message = "El asegurado ya está registrado." });
                }

                _context.Asegurados.Add(asegurado);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Asegurado {asegurado.PrimerNombre} {asegurado.PrimerApellido} registrado con éxito." });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                return StatusCode(500, new { message = $"Ocurrió un error al registrar el asegurado. Por favor, intente nuevamente más tarde. {ex.Message}" });
            }
        }

        // DELETE: api/Asegurado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsegurado(int id)
        {
            try
            {
                var asegurado = await _context.Asegurados.FindAsync(id);
                if (asegurado == null)
                {
                    return NotFound(new { message = $"Asegurado con ID {id} no encontrado." });
                }

                _context.Asegurados.Remove(asegurado);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Asegurado con ID {id} eliminado con éxito." });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                return StatusCode(500, new { message = $"Ocurrió un error al eliminar el asegurado. Por favor, intente nuevamente más tarde. {ex.Message}" });
            }
        }

        private bool AseguradoExists(int id)
        {
            return _context.Asegurados.Any(e => e.NumeroIdentificacion == id);
        }
    }
}
