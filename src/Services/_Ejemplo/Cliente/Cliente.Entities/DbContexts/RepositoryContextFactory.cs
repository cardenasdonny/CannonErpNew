using Cliente.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Entities.DbContexts
{
    public class RepositoryContextFactory : DbContext
    {
        public RepositoryContextFactory(
            DbContextOptions<RepositoryContextFactory> options
        )
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("Cliente");

            // Model Contraints
            ModelConfig(builder);
        }

        public Task AddAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public DbSet<Cliente> Clientes { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ClienteConfiguration(modelBuilder.Entity<Cliente>());
        }
    }
}
