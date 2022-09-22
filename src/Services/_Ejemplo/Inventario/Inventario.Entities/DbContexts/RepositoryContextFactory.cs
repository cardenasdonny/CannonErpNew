using Inventario.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Entities.DbContexts
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
            builder.HasDefaultSchema("Inventario");
            #endregion
            ModelConfiguration(builder);
        }
        private void ModelConfiguration(ModelBuilder modelBuilder)
        {
            new ArticuloConfiguration(modelBuilder.Entity<Articulo>());
            new ExistenciaConfiguration(modelBuilder.Entity<Existencia>());
        }


        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Existencia> Existencias { get; set; }
    }
}
