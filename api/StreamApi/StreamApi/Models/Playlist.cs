using System.ComponentModel.DataAnnotations;

namespace StreamApi.Models
{
    public class Playlist
    {
        public int ID { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        public List<ItemPlaylist> Items { get; set; } = new();
    }
}
