using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido.Entities.Configuration
{
    public class PedidoConfiguration
    {
        public PedidoConfiguration(EntityTypeBuilder<Pedido> entityBuilder)
        {
            entityBuilder.HasKey(x => x.PedidoId);
        }
    }
}
