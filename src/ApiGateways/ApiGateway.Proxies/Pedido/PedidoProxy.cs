using ApiGateway.Models;
using ApiGateway.Models.Pedido.Commands;
using ApiGateway.Models.Pedido.DataTransferObjects;
using ApiGateway.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;


namespace ApiGateway.Proxies.Pedido
{
    public interface IPedidoProxy
    {
        Task<DataCollection<PedidoDto>> GetAllAsync(int page, int take);
        Task<PedidoDto> GetByIdAsync(int id);
        Task CreateAsync(PedidoCreateCommand command);
    }

    public class PedidoProxy : IPedidoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public PedidoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            //httpClient.AddBearerToken(httpContextAccessor);
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<PedidoDto>> GetAllAsync(int page, int take)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PedidoUrl}v1/EjemploPedido/Pedidos/?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<PedidoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<PedidoDto> GetByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PedidoUrl}v1/EjemploPedido/Pedidos/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<PedidoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task CreateAsync(PedidoCreateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );

            var request = await _httpClient.PostAsync($"{_apiUrls.PedidoUrl}v1/EjemploPedido/Pedidos", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
