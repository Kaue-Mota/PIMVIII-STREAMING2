using System.ComponentModel.DataAnnotations;

namespace StreamApi.Models
{
    public class Conteudo
    {
        public int ID { get; set; }

        [Required, MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Tipo { get; set; } = string.Empty;

        public int CriadorID { get; set; }
        public Criador? Criador { get; set; }
    }
}
