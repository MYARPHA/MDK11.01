using CosmeticStoreLib.Models;
using System.Net.Http.Json;

namespace CosmeticStoreLib.Services
{
    public class ProductService
    {

        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5103/api/";

        public ProductService()
        {
            _client = new() { BaseAddress = new Uri(_baseUrl) };
        }

        // Получение списка продуктов
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _client.GetAsync("Products/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }

        // Получение продукта из списка по ID
        public async Task<Product> GetProductByIdAsync(string productArticle)
        {
            var response = await _client.GetAsync($"Products/{productArticle}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        // Добавление нового продукта
        public async Task AddProductAsync(Product product)
        {
            var response = await _client.PostAsJsonAsync("Products/", product);
            response.EnsureSuccessStatusCode();
        }
        
        // Обновление сущетсвующего продукта
        public async Task UpdateProductAsync(Product product)
        {
            var response = await _client.PutAsJsonAsync($"Products/{product.ProductArticleNumber}", product);
            response.EnsureSuccessStatusCode();
        }

        // Удаление продукта
        public async Task DeleteProductAsync(string productArticle)
        {
            var response = await _client.DeleteAsync($"Products/{productArticle}");
            response.EnsureSuccessStatusCode();
        }
    }
}
