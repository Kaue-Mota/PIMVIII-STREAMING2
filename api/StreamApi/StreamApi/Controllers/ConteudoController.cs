using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamApi.Data;
using StreamApi.Models;

namespace StreamApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConteudoController : ControllerBase
    {
        private readonly StreamingContext _context;
        public ConteudoController(StreamingContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Conteudos.Include(c => c.Criador).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Conteudos.Include(c => c.Criador).FirstOrDefaultAsync(c => c.ID == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Conteudo conteudo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var criadorExists = await _context.Criadores.AnyAsync(c => c.ID == conteudo.CriadorID);
            if (!criadorExists) return BadRequest(new { message = $"Criador {conteudo.CriadorID} não existe." });

            _context.Conteudos.Add(conteudo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = conteudo.ID }, conteudo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Conteudo conteudo)
        {
            if (id != conteudo.ID) return BadRequest();
            _context.Conteudos.Update(conteudo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Conteudos.FindAsync(id);
            if (c == null) return NotFound();
            _context.Conteudos.Remove(c);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
