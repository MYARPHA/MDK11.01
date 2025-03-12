using LabWork20.Data;
using LabWork20.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork20.Services
{
    public class UserService
    {
        private readonly CinemaContext _context = new();

        public async Task<CinemaUser?> GetUserAsync(string login, string password)
        {
            var user = await _context.CinemaUsers.SingleOrDefaultAsync(u => u.Login == login);

            if (user?.Password == password)
            {
                return user;
            }

            return null;
        }
    }
}
