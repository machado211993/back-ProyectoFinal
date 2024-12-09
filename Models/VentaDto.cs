using System.Collections.Generic;

namespace ProductCategoryCrud.Models
{
    public class VentaDto
    {
        public decimal Total { get; set; }
        public int UserId { get; set; }

        public List<VentaProductoDto> Productos { get; set; }
    }

    public class VentaProductoDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Nuevo campo
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
