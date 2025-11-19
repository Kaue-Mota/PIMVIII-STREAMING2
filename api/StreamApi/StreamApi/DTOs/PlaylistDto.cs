using System.Collections.Generic;

namespace StreamApi.DTOs
{
    public class PlaylistDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public int UsuarioId { get; set; }

        public List<ItemPlaylistDto> Items { get; set; } = new();
    }
}
