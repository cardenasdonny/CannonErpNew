using ApiGateway.Models.Cliente.DataTransferObjects;
using ApiGateway.Proxies.Config;
using ApiGateway.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ApiGateway.Proxies.Cliente
{
    public interface IClienteProxy
    {
        Task<DataCollection<ClienteDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClienteDto> GetByIdAsync(int id);
    }

    public class ClienteProxy : IClienteProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public ClienteProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<ClienteDto>> GetAllAsync(int page, int take, IEnumerable<int> clientes = null)
        {
            var ids = string.Join(',', clientes ?? new List<int>());

            var request = await _httpClient.GetAsync($"{_apiUrls.ClienteUrl}v1/EjemploCliente/Clientes/?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ClienteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.ClienteUrl}v1/EjemploCliente/Clientes/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ClienteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

    }
}
