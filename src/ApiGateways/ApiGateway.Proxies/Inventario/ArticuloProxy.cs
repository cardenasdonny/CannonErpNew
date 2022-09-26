using ApiGateway.Models;
using ApiGateway.Models.Inventario.DataTransferObjects;
using ApiGateway.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ApiGateway.Proxies.Inventario
{
    public interface IArticuloProxy
    {
        Task<DataCollection<ArticuloDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ArticuloDto> GetByIdAsync(int id);
    }

    public class ArticuloProxy : IArticuloProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public ArticuloProxy(
            HttpClient httpClient,
            IOptions<ApiUrls> apiUrls,
            IHttpContextAccessor httpContextAccessor)
        {
            //httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<ArticuloDto>> GetAllAsync(int page, int take, IEnumerable<int> clientes = null)
        {
            var ids = string.Join(',', clientes ?? new List<int>());

            var request = await _httpClient.GetAsync($"{_apiUrls.InventarioUrl}v1/EjemploInventario/Articulos/?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ArticuloDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ArticuloDto> GetByIdAsync(int id)        {
           
            var request = await _httpClient.GetAsync($"{_apiUrls.InventarioUrl}v1/EjemploInventario/Articulos/{id}");
            
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ArticuloDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
