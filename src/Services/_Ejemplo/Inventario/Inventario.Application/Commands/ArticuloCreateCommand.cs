using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Commands
{
    public class ArticuloCreateCommand: INotification
    {

        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
