using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakX.API.Data;
using SneakX.API.Models;

namespace SneakX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly SneakXContext _context;

        public ProductsController(SneakXContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
    string? brand,
    string? gender,
    string? category,
    string? search,
    decimal? minPrice,
    decimal? maxPrice,
    string? sortBy,
    string? sortOrder
    //int pageNumber = 1,
    //int pageSize = 10)
    )
        {
            var query = _context.Products.AsQueryable();

            // Filters
            if (!string.IsNullOrEmpty(brand))
                query = query.Where(p => p.Brand.ToLower() == brand.ToLower());

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(p => p.Gender.ToLower() == gender.ToLower());

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.ToLower() == category.ToLower());

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p =>
                    p.Name.ToLower().Contains(search.ToLower()) ||
                    p.Brand.ToLower().Contains(search.ToLower()));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                bool descending = sortOrder?.ToLower() == "desc";

                query = sortBy.ToLower() switch
                {
                    "name" => descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "brand" => descending ? query.OrderByDescending(p => p.Brand) : query.OrderBy(p => p.Brand),
                    _ => query // if unknown sort field
                };
            }

            // Count
            //var totalItems = await query.CountAsync();
            //var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Headers
            //Response.Headers.Add("X-Total-Count", totalItems.ToString());
            //Response.Headers.Add("X-Total-Pages", totalPages.ToString());

            // Pagination
            var products = await query
                //.Skip((pageNumber - 1) * pageSize)
                //.Take(pageSize)
                .ToListAsync();

            return Ok(products);
        }


        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return product;
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/products/slug/nike-air-zoom-pegasus-37
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Product>> GetBySlug(string slug)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Slug == slug);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

    }
}
