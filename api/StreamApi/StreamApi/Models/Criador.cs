using System.ComponentModel.DataAnnotations;

namespace StreamApi.Models
{
    public class Criador
    {
        public int ID { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        public List<Conteudo> Conteudos { get; set; } = new();
    }
}
