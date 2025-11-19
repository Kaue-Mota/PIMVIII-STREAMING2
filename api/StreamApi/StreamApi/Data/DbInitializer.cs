using StreamApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StreamApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(StreamingContext context)
        {
            // Garante que o banco existe e está migrado
            context.Database.Migrate();

            // Se já tiver usuário, assumimos que está populado e saímos
            if (context.Usuarios.Any())
                return;

            // 1) Usuário
            var usuario = new Usuario
            {
                Nome = "Usuário de Teste",
                Email = "usuario@teste.com"
            };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            // 2) Criador
            var criador = new Criador
            {
                Nome = "Criador de Teste"
            };
            context.Criadores.Add(criador);
            context.SaveChanges();

            // 3) Conteúdo
            var conteudo = new Conteudo
            {
                Titulo = "Filme de Teste",
                Tipo = "Filme",
                CriadorID = criador.ID
            };
            context.Conteudos.Add(conteudo);
            context.SaveChanges();

            // 4) Playlist + item
            var playlist = new Playlist
            {
                Nome = "Playlist de Teste",
                UsuarioID = usuario.ID,
                Items = new List<ItemPlaylist>
                {
                    new ItemPlaylist
                    {
                        ConteudoID = conteudo.ID
                    }
                }
            };

            context.Playlists.Add(playlist);
            context.SaveChanges();
        }
    }
}
