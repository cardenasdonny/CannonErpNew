using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Pedido.Proxies.Inventario.Commands;
using System.Text;
using System.Text.Json;

namespace Pedido.Proxies.Inventario
{
    public interface IInventarioProxy
    {
        Task ExistenciaUpdateAsync(ExistenciaUpdateCommand command);
    }
    public class InventarioProxy : IInventarioProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public InventarioProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IConfiguration configuration)
        {
            _apiUrls = apiUrls.Value;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public HttpClient HttpClient { get; }

        

        public async Task ExistenciaUpdateAsync(ExistenciaUpdateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );           

            var request = await _httpClient.PutAsync(_apiUrls.InventarioUrl, content);
            
            request.EnsureSuccessStatusCode();
        }
       
    }
}
