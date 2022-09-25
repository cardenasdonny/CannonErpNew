using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido.Entities.Configuration
{
    public class PedidoDetalleConfiguration
    {
        public PedidoDetalleConfiguration(EntityTypeBuilder<PedidoDetalle> entityBuilder)
        {
            entityBuilder.HasKey(x => x.PedidoDetalleId);
        }
    }
}
