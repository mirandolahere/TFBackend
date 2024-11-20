﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSqlServer.Data;
using ApiSqlServer.Models;

namespace ApiSqlServerExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAvalability>>> GetProductos()
        {
            var availableProducts = await _context.Productos
                                       .Join(_context.Disponibilidad,
                                             p => p.Disponibilidad,
                                             pa => pa.Id,
                                             (p, pa) => new ProductAvalability
                                             {
                                                 Id = p.Id,
                                                 Nombre = p.Nombre,
                                                 Descripcion = p.Descripcion,
                                                 Precio = p.Precio,
                                                Cantidad = p.Cantidad,
                                                ImagenBase64 =p.ImagenBase64,
                                                Disponibilidad = pa.Nombre,
                                                fechaExpiracion = p.fechaExpiracion
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

