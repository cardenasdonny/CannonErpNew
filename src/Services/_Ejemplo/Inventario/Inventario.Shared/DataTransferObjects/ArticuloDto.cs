using Inventario.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Shared.DataTransferObjects
{
    public class ArticuloDto
    {
        public int ArticuloId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public Existencia Stock { get; set; }
    }
}
