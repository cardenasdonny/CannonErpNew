using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Collection
{
    #region Clase que contiene las propiedades de la paginación
    public class DataCollection<T>
    {
        public bool HasItems
        {
            get { return Items != null && Items.Any(); }
        }
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
    }
    #endregion
}
