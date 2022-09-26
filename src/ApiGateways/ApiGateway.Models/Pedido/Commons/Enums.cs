using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Models.Pedido.Commons
{
    public class Enums
    {
        public enum PedidoEstado
        {
            Cancelada,
            Pendiente,
            Aprobada
        }

        public enum MetodoPago
        {
            TarjetaCredito,
            PayPal,
            Efectivo
        }


        public enum ExistenciaAction
        {
            Add,
            Substract
        }

    }
}
