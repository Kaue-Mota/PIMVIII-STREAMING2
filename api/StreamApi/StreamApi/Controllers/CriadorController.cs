using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamApi.Data;
using StreamApi.Models;

namespace StreamApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriadorController : ControllerBase
    {
        private readonly StreamingContext _context;
        public CriadorController(StreamingContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Criadores.Include(c => c.Conteudos).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Criadores.Include(c => c.Conteudos).FirstOrDefaultAsync(c => c.ID == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Criador criador)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Criadores.Add(criador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = criador.ID }, criador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Criador criador)
        {
            if (id != criador.ID) return BadRequest();
            _context.Criadores.Update(criador);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Criadores.FindAsync(id);
            if (c == null) return NotFound();
            _context.Criadores.Remove(c);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
