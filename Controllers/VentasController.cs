using Microsoft.AspNetCore.Mvc;
using ProductCategoryCrud.Data;
using ProductCategoryCrud.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


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

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] VentaDto ventaDto)
        {
            try
            {
                // Validar que el DTO no sea nulo
                if (ventaDto == null)
                {
                    return BadRequest("Los datos de la venta son incorrectos.");
                }

                // Validar que el UserId exista en la base de datos
                var user = await _context.Users.FindAsync(ventaDto.UserId);
                if (user == null)
                {
                    return BadRequest("El usuario no existe.");
                }

                // Crear la venta
                var venta = new Venta
                {
                    Fecha = DateTime.Now,
                    Total = ventaDto.Total,
                    UserId = ventaDto.UserId
                };

                // Agregar la venta al contexto y guardar los cambios
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                // Validar si la lista de productos no es vacía
                if (ventaDto.Productos == null || !ventaDto.Productos.Any())
                {
                    return BadRequest("Debe proporcionar al menos un producto para la venta.");
                }

                // Agregar los productos de la venta
                foreach (var item in ventaDto.Productos)
                {
                    // Buscar el producto en la base de datos para obtener el nombre y otros detalles
                    var product = await _context.Products.FindAsync(item.ProductId);

                    if (product == null)
                    {
                        return BadRequest($"El producto con ID {item.ProductId} no existe.");
                    }

                    var ventaProducto = new VentaProducto
                    {
                        VentaId = venta.Id,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName ?? product.Name, // Asignar el nombre del producto desde el DTO o la base de datos
                        Cantidad = item.Cantidad,
                        Precio = item.Precio
                    };

                    _context.VentaProductos.Add(ventaProducto);
                }

                // Guardar los cambios en los productos de la venta
                await _context.SaveChangesAsync();

                // Retornar un código de estado 204 No Content (sin respuesta de datos)
                return StatusCode(204); // No content, solo confirmación de éxito sin contenido adicional.
            }
            catch (Exception ex)
            {
                // Manejar el error de manera genérica y no exponer detalles
                return StatusCode(500); // Error interno del servidor, sin detalles
            }
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

        // Nuevo método para listar todas las ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            return await _context.Ventas.ToListAsync();
        }
        
    }
}
