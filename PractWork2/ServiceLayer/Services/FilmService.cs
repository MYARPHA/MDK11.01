using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class FilmService
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress = "http://localhost:5049/api/Films";

        public FilmService()
        {
            _client = new() { BaseAddress = new Uri(_baseAddress) };
        }

        // список фильмов
        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            return await _client.GetFromJsonAsync<IEnumerable<Film>>("Films");
        }

    }
}
