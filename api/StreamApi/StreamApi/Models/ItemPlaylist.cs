using System.Text.Json.Serialization;

namespace StreamApi.Models
{
    public class ItemPlaylist
    {
        public int PlaylistID { get; set; }

        [JsonIgnore]              // evita o ciclo ao serializar
        public Playlist? Playlist { get; set; }

        public int ConteudoID { get; set; }
        public Conteudo? Conteudo { get; set; }
    }
}
