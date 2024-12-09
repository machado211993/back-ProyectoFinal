using Microsoft.AspNetCore.Mvc;
using ProductCategoryCrud.Data;
using ProductCategoryCrud.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCategoryCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Ventas
        [HttpPost]
        public async Task<ActionResult<Venta>> CrearVenta([FromBody] VentaDto ventaDto)
        {
            // Crear la venta
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Total = ventaDto.Total,
                UserId = ventaDto.UserId
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            // Agregar los productos de la venta
            foreach (var item in ventaDto.Productos)
            {
                // Buscar el producto en la base de datos para obtener el nombre
                var product = await _context.Products.FindAsync(item.ProductId);

                if (product == null)
                {
                    return BadRequest($"El producto con ID {item.ProductId} no existe.");
                }

                var ventaProducto = new VentaProducto
                {
                    VentaId = venta.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName, // Asignar el nombre del producto
                    Cantidad = item.Cantidad,
                    Precio = item.Precio
                };

                _context.VentaProductos.Add(ventaProducto);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenta), new { id = venta.Id }, venta);
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }
    }
}
