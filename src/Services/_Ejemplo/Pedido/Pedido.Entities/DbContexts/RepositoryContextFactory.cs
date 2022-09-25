using Microsoft.EntityFrameworkCore;
using Pedido.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido.Entities.DbContexts
{
    public class RepositoryContextFactory : DbContext
    {
        public RepositoryContextFactory(DbContextOptions<RepositoryContextFactory> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region El esquema de la base de datos
            builder.HasDefaultSchema("Pedido");
            #endregion
            ModelConfiguration(builder);
        }
        private void ModelConfiguration(ModelBuilder modelBuilder)
        {
            new PedidoConfiguration(modelBuilder.Entity<Pedido>());
            new PedidoDetalleConfiguration(modelBuilder.Entity<PedidoDetalle>());
        }


        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
    }
}
