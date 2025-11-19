using System.ComponentModel.DataAnnotations;

namespace StreamApi.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        public List<Playlist> Playlists { get; set; } = new();
    }
}
