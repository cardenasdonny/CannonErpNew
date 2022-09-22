namespace Inventario.Entities
{
    public class Articulo
    {
        public int ArticuloId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public Existencia Stock { get; set; }
    }
}
