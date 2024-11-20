using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSqlServer.Data;
using ApiSqlServer.Models;

namespace ApiSqlServerExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetProductos()
        {
            var availableProducts = await _context.Order
                                       .Join(_context.Productos,
                                             p => p.ProductoId,
                                             pa => pa.Id,
                                             (p, pa) => new { Order = p, Producto = pa })
                                       .Join(_context.EstatusDelivery,
                                        op => op.Order.Status,  // Clave de unión en la primera proyección 'Order'
                                         c => c.Id,
                                         (op, c) => new OrderDetail
                                         {
                                                 Id = op.Order.Id,
                                                 ProductoId = op.Order.ProductoId,
                                                 ProductoNombre = op.Producto.Nombre,
                                                 Cantidad = op.Order.Cantidad,
                                                 PrecioTotal = op.Order.PrecioTotal,
                                                 FechaLlegada = op.Order.FechaLlegada,
                                                 Status = c.Nombre
    })
                                       .ToListAsync();

            return availableProducts;
        }

        // GET: api/productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // POST: api/productos
        [HttpPost]
        public async Task<ActionResult<Product>> PostProducto(Product producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // PUT: api/productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Product producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

