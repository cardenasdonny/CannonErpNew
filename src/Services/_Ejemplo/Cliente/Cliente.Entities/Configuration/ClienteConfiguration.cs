using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Entities.Configuration
{
    public class ClienteConfiguration
    {
        public ClienteConfiguration(EntityTypeBuilder<Cliente> entityBuilder)
        {
            entityBuilder.HasKey(x => x.ClienteId);
            entityBuilder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);

            var clients = new List<Cliente>();

            for (var i = 1; i <= 10; i++)
            {
                clients.Add(new Cliente
                {
                    ClienteId = i,
                    Nombre = $"Cliente {i}"
                });
            }

            entityBuilder.HasData(clients);
        }
    }
}
