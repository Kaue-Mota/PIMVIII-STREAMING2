using Microsoft.EntityFrameworkCore;
using StreamApi.Data;
using StreamApi.Models;
using StreamApi.Repositories.Interfaces;

namespace StreamApi.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly StreamingContext _context;
        public PlaylistRepository(StreamingContext context) => _context = context;

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            return await _context.Playlists
                        .Include(p => p.Items)
                            .ThenInclude(i => i.Conteudo)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<Playlist?> GetByIdAsync(int id)
        {
            return await _context.Playlists
                        .Include(p => p.Items)
                            .ThenInclude(i => i.Conteudo)
                        .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<Playlist> AddAsync(Playlist playlist)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));

            var userExists = await _context.Usuarios.AnyAsync(u => u.ID == playlist.UsuarioID);
            if (!userExists) throw new InvalidOperationException($"Usuário com ID {playlist.UsuarioID} não existe.");

            foreach (var item in playlist.Items)
            {
                var conteudo = await _context.Conteudos.FindAsync(item.ConteudoID);
                if (conteudo != null) item.Conteudo = conteudo;
            }

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        public async Task UpdateAsync(Playlist playlist)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));

            var existing = await _context.Playlists
                                   .Include(p => p.Items)
                                   .FirstOrDefaultAsync(p => p.ID == playlist.ID);
            if (existing == null) throw new InvalidOperationException($"Playlist com ID {playlist.ID} não existe.");

            existing.Nome = playlist.Nome;
            existing.UsuarioID = playlist.UsuarioID;

            var incomingKeys = playlist.Items.Select(i => (i.PlaylistID, i.ConteudoID)).ToHashSet();
            var existingKeys = existing.Items.Select(i => (i.PlaylistID, i.ConteudoID)).ToHashSet();

            var toRemove = existing.Items.Where(i => !incomingKeys.Contains((i.PlaylistID, i.ConteudoID))).ToList();
            foreach (var rem in toRemove) existing.Items.Remove(rem);

            var toAdd = playlist.Items.Where(i => !existingKeys.Contains((i.PlaylistID, i.ConteudoID))).ToList();
            foreach (var add in toAdd)
            {
                var conteudo = await _context.Conteudos.FindAsync(add.ConteudoID);
                if (conteudo != null) add.Conteudo = conteudo;
                existing.Items.Add(add);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _context.Playlists.FindAsync(id);
            if (p != null)
            {
                _context.Playlists.Remove(p);
                await _context.SaveChangesAsync();
            }
        }
    }
}
