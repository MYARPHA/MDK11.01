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
        public async Task<IEnumerable<Film>?> GetAllFilmsAsync()
        {
            return await _client.GetFromJsonAsync<IEnumerable<Film>>("Films");
        }

        // получить фильм по Id
        public async Task<Film?> GetFilmByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<Film>($"Films/{id}");
        }

        public async Task AddFilmAsync(Film film)
        {
            var response = await _client.PostAsJsonAsync("Films", film);
            response.EnsureSuccessStatusCode(); 
        }

        public async Task<bool>DeleteFilmAsync(int id)
        {
            var response = await _client.DeleteAsync($"Films/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
