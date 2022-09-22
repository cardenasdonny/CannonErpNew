using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Shared.DataTransferObjects
{
    public class ExistenciaDto
    {
        public int ExistenciaId { get; set; }
        public int ArticuloId { get; set; }
        public int Stock { get; set; }
    }
}
