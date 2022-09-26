using ApiGateway.Proxies;
using ApiGateway.Proxies.Cliente;
using ApiGateway.Proxies.Inventario;
using ApiGateway.Proxies.Pedido;

namespace Api.Gateway.Web.Config
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddAppsettingBinding(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));
            return service;
        }

        public static IServiceCollection AddProxiesRegistration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();

            service.AddHttpClient<IPedidoProxy, PedidoProxy>();
            service.AddHttpClient<IClienteProxy, ClienteProxy>();
            service.AddHttpClient<IArticuloProxy, ArticuloProxy>();

            return service;
        }
    }
}
