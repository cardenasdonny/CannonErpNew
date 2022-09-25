using Inventario.Application.Commands;
using Inventario.Entities;
using Inventario.Entities.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Handlers
{
    public class ExistenciaUpdateEventHandler
        : INotificationHandler<ExistenciaUpdateCommand>
    {
        private readonly RepositoryContextFactory _context;
        private readonly ILogger<ExistenciaUpdateEventHandler> _logger;
        public ExistenciaUpdateEventHandler(RepositoryContextFactory context, ILogger<ExistenciaUpdateEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Handle(ExistenciaUpdateCommand notificacion, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- ExistenciaUpdateCommand started ---");

            var articulos = notificacion.Items.Select(x => x.ArticuloId);
            var existencias = await _context.Existencias.Where(x => articulos.Contains(x.ArticuloId)).ToListAsync();

            _logger.LogInformation("--- Obteniendo articulos de la DB ---");

            foreach (var item in notificacion.Items)
            {
                var entry = existencias.SingleOrDefault(x => x.ArticuloId == item.ArticuloId);
                if(item.Action == Shared.Common.Enums.ExistenciaAction.Substract)
                {
                    if (entry == null || item.Stock > entry.Stock) { 
                        _logger.LogError($"--- El Articulo { entry.ArticuloId} - no tiene suficiente stock ---");
                        throw new Exception($"--- El Articulo {entry.ArticuloId} - no tiene suficiente stock ---");
                    }
                    entry.Stock -= entry.Stock;
                    _logger.LogInformation($"--- Stock descontado, stock nuevo: {entry.Stock}---");
                }
                else
                {
                    if(entry == null)
                    {
                        entry = new Existencia
                        {
                            ArticuloId = item.ArticuloId,
                        };
                        await _context.AddAsync(entry);
                        _logger.LogInformation($"--- Se crea un nuevo stock para {entry.ArticuloId} ---");
                    }
                    entry.Stock += item.Stock;
                    _logger.LogInformation($"--- Stock incrementado , stock nuevo: {entry.Stock} --- ");

                }
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("--- ExistenciaUpdateCommand ended ---");
        }
    }
}
