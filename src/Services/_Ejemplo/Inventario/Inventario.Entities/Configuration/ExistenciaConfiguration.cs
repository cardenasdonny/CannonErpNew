using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Entities.Configuration
{
    public class ExistenciaConfiguration
    {
        public ExistenciaConfiguration(EntityTypeBuilder<Existencia> entityBuilder)
        {
            entityBuilder.HasKey(x => x.ExistenciaId);

            #region Seed Existencia
            var existencias = new List<Existencia>();
            var random = new Random();
            for (int i = 1; i <= 100; i++)
            {
                existencias.Add(new Existencia
                {
                    ExistenciaId = i,
                    ArticuloId = i,
                    Stock = random.Next(0, 20)
                });
            }
            entityBuilder.HasData(existencias);
            #endregion
        }
    }
}
