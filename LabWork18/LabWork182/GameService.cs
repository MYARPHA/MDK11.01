using LabWork182.Data;
using LabWork182.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork182
{
    public class GameService
    {
        private readonly GameContext _context = new();

        public async Task<List<Game>> GetAllGamesAsync()
            => await _context.Games.Include(g => g.Category).ToListAsync();

        public async Task AddGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGameAsync(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}
