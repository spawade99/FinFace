using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        [HttpGet]
        [Route("GetStocks")]
        public async Task<ActionResult<IEnumerable<Models.Stock>>> GetStocks()
        {
            return await _context.Stocks.ToListAsync();
        }

        [HttpGet]
        [Route("GetStock/{id}")]
        public async Task<ActionResult<Models.Stock>> GetStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

    }
}