using Microsoft.AspNetCore.Mvc;
using StreamApi.DTOs;
using StreamApi.Models;
using StreamApi.Repositories.Interfaces;

namespace StreamApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistRepository _repo;

        public PlaylistController(IPlaylistRepository repo)
        {
            _repo = repo;
        }

        
        private static PlaylistDto ToDto(Playlist p)
        {
            return new PlaylistDto
            {
                Id = p.ID,
                Nome = p.Nome,
                UsuarioId = p.UsuarioID,
                Items = p.Items.Select(i => new ItemPlaylistDto
                {
                    ConteudoId = i.ConteudoID,
                    ConteudoTitulo = i.Conteudo?.Titulo ?? string.Empty,
                    ConteudoTipo = i.Conteudo?.Tipo ?? string.Empty
                }).ToList()
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var playlists = await _repo.GetAllAsync();
            var dtos = playlists.Select(ToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(ToDto(p));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Playlist playlist)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var created = await _repo.AddAsync(playlist);
                return CreatedAtAction(nameof(Get), new { id = created.ID }, ToDto(created));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Playlist playlist)
        {
            if (id != playlist.ID) return BadRequest();

            try
            {
                await _repo.UpdateAsync(playlist);
                var updated = await _repo.GetByIdAsync(id);
                if (updated == null) return NotFound();
                return Ok(ToDto(updated));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
