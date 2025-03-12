using LabWork20.Data;
using LabWork20.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork20.Services
{
    public class CinemaService
    {
        private readonly CinemaContext _context = new();

        public async Task<List<Film>> GetAllFilmsAsync()
            => await _context.Films.ToListAsync();
    }
}
