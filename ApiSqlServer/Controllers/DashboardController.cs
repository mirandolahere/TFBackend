using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSqlServer.Data;
using ApiSqlServer.Models;

namespace ApiSqlServerExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public  ActionResult<DashboardCombinados> GetReport()
        {
            var ventas =  _context.Order.
                GroupBy(v => new { v.FechaPedido.Year, v.FechaPedido.Month })
                .Select(g => new
                {
                    Año = g.Key.Year,
                    Mes = g.Key.Month,
                    TotalVentas = g.Count(),
                    MontoTotal = g.Sum(v => v.PrecioTotal)
                })
                .OrderBy(g => g.Año).ThenBy(g => g.Mes) // Ordenar por Año y Mes
                .ToList();

            var compras = _context.Vendedor.
               GroupBy(v => new { v.FechaPedido.Year, v.FechaPedido.Month })
               .Select(g => new
               {
                   Año = g.Key.Year,
                   Mes = g.Key.Month,
                   TotalVentas = g.Count(),
                   MontoTotal = g.Sum(v => v.PrecioTotal)
               })
       .OrderBy(g => g.Año).ThenBy(g => g.Mes) // Ordenar por Año y Mes
       .ToList();

            var datosCombinados = new 
            {
                CompraPorMes = compras,
                VentaPorMes = ventas
            };


            return Ok(datosCombinados);
        }

        [HttpGet("top-selling")]
        public IActionResult ObtenerVentasMayores()
        {
            var reporteVentasPorMes = _context.Order
                                       .Join(_context.Productos,
                                             p => p.ProductoId,
                                             pa => pa.Id,
                                             (p, pa) => new 
                                             {
                                                 Nombre = pa.Nombre,
                                                 CantidadVendida = p.Cantidad,
                                                 Precio = p.PrecioTotal
                                             }).OrderBy(v => v.CantidadVendida)// Ordenar por fecha (puedes cambiar esto según tu criterio)
            .Take(5)  // Obtener solo las primeras 3 filas
            .ToList();
                                   

            return Ok(reporteVentasPorMes);
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

