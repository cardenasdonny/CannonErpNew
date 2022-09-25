
using MediatR;
using Microsoft.Extensions.Logging;
using Pedido.Application.Commands;
using Pedido.Entities;
using Pedido.Entities.DbContexts;
using Pedido.Proxies.Inventario;
using Pedido.Proxies.Inventario.Commands;
using Pedido.Shared.Common;
using static Pedido.Shared.Common.Enums;

namespace Pedido.Application.Handlers
{
    public class PedidoCreateEventHandler :
        INotificationHandler<PedidoCreateCommand>
    {
        private readonly RepositoryContextFactory _context;
        private readonly IInventarioProxy _inventarioProxy;
        private readonly ILogger<PedidoCreateEventHandler> _logger;

        public PedidoCreateEventHandler(RepositoryContextFactory context, IInventarioProxy inventarioProxy, ILogger<PedidoCreateEventHandler> logger)
        {
            _context = context;
            _inventarioProxy = inventarioProxy;
            _logger = logger;
        }

        public async Task Handle(PedidoCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- Nuevo pedido creation started");
            var entry = new Entities.Pedido();

            using (var trx = await _context.Database.BeginTransactionAsync()) // si falla algo se hace un rollback
            {
                // 01. Prepare detail (esto se puede mapear mejor con automaper)
                _logger.LogInformation("--- Preparing detail");
                PrepareDetail(entry, notification);

                // 02. Prepare header
                _logger.LogInformation("--- Preparing header");
                PrepareHeader(entry, notification);

                // 03. Create order
                _logger.LogInformation("--- Creating order");
                await _context.AddAsync(entry,cancellationToken);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"--- Order {entry.PedidoId} was created");

                // Debe ir al microservicio de inventario y actualizar la existencia

                // 04. Update Stocks
                _logger.LogInformation("--- Updating stock");
                await _inventarioProxy.ExistenciaUpdateAsync(new ExistenciaUpdateCommand
                {
                    Items = notification.Items.Select(x => new ExistenciaUpdateItem
                    {
                        ArticuloId = x.ArticuloId,
                        Stock = x.Cantidad,
                        Action = ExistenciaAction.Substract
                    })
                });

                await trx.CommitAsync();
            }

            _logger.LogInformation("--- New order creation ended");
        }

        private void PrepareDetail(Entities.Pedido entry, PedidoCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new PedidoDetalle
            {
                ArticuloId = x.ArticuloId,
                Cantidad = x.Cantidad,
                PrecioUnitario = x.PrecioUnitario,
                Total = x.PrecioUnitario * x.Cantidad
            }).ToList();
        }

        private void PrepareHeader(Entities.Pedido entry, PedidoCreateCommand notification)
        {
            // Header information
            entry.PedidoEstado = Enums.PedidoEstado.Pendiente;
            entry.MetodoPago = notification.MetodoPago;
            entry.ClienteId = notification.ClienteId;
            entry.FechaCreacion = DateTime.UtcNow;

            // Sum
            entry.Total = entry.Items.Sum(x => x.Total);
        }
    }
    }
