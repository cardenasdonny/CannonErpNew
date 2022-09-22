using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Entities.Configuration
{
    public class ArticuloConfiguration
    {
        public ArticuloConfiguration(EntityTypeBuilder<Articulo> entityBuilder)
        {
            entityBuilder.Property(x => x.ArticuloId);
            entityBuilder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);

            #region Seed Articulos
            var articulos = new List<Articulo>();
            var random = new Random();
            for (int i = 1; i <= 100; i++)
            {
                articulos.Add(new Articulo
                {
                    ArticuloId = i,
                    Nombre = $"Articulo {i}",
                    Precio = random.Next(100, 1000),
                });
            }
            entityBuilder.HasData(articulos);
            #endregion
        }
    }
}
