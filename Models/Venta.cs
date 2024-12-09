namespace ProductCategoryCrud.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<VentaProducto> VentaProductos { get; set; }
    }

    public class VentaProducto
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; } // Nuevo campo
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
