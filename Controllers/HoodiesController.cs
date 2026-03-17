using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakX.API.Data;
using SneakX.API.Models;

namespace SneakX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoodiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HoodiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hoodie>>> GetHoodies()
        {
            try
            {
                var hoodies = await _context.Hoodies.ToListAsync();
                return Ok(hoodies);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hoodies error: " + ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hoodie>> GetHoodie(int id)
        {
            try
            {
                var hoodie = await _context.Hoodies.FindAsync(id);

                if (hoodie == null)
                    return NotFound();

                return Ok(hoodie);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hoodie by id error: " + ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}