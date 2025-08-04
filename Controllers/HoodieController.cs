using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakX.API.Data;
using SneakX.API.Models;

namespace SneakX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoodieController : ControllerBase
    {
        private readonly SneakXContext _context;

        public HoodieController(SneakXContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hoodie>>> GetHoodies()
        {
            return await _context.Hoodies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hoodie>> GetHoodie(int id)
        {
            var hoodie = await _context.Hoodies.FindAsync(id);
            if (hoodie == null)
                return NotFound();

            return hoodie;
        }
    }
}
