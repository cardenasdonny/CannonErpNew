using Microsoft.AspNetCore.Http;
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

        public InventarioProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            //httpClient.AddBearerToken(httpContextAccessor);
            _apiUrls = apiUrls.Value;
            _httpClient = httpClient;
            
        }

        public HttpClient HttpClient { get; }

        

        public async Task ExistenciaUpdateAsync(ExistenciaUpdateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );           

            var request = await _httpClient.PutAsync(_apiUrls.InventarioUrl + "v1/EjemploInventario/Existencias", content);
            
            request.EnsureSuccessStatusCode();
        }
       
    }
}
