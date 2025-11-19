using Microsoft.EntityFrameworkCore;
using StreamApi.Models;

namespace StreamApi.Data
{
    public class StreamingContext : DbContext
    {
        public StreamingContext(DbContextOptions<StreamingContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Criador> Criadores { get; set; } = null!;
        public DbSet<Conteudo> Conteudos { get; set; } = null!;
        public DbSet<Playlist> Playlists { get; set; } = null!;
        public DbSet<ItemPlaylist> ItemPlaylists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // chave composta
            modelBuilder.Entity<ItemPlaylist>()
                .HasKey(ip => new { ip.PlaylistID, ip.ConteudoID });

            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Items)
                .WithOne(i => i.Playlist)
                .HasForeignKey(i => i.PlaylistID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Criador>()
                .HasMany(c => c.Conteudos)
                .WithOne(cnt => cnt.Criador)
                .HasForeignKey(cnt => cnt.CriadorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
