using api.Data;
using api.DTO.Stock;
using api.Mapper;
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
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStocks()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet]
        [Route("GetStock/{id}")]
        public async Task<ActionResult<StockDto>> GetStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock.ToStockDto();
        }

        [HttpPost]
        [Route("CreateStock")]
        public async Task<ActionResult<StockDto>> CreateStock([FromBody] CreateStockDto stockDto)
        {
            var stock = stockDto.ToStockModelFromCreateStockDto();
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("UpdateStock/{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] CreateStockDto stockDto)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Industry = stockDto.Industry;
            stock.LastDiv = stockDto.LastDiv;
            stock.MarketCap = stockDto.MarketCap;
            stock.Purchase = stockDto.Purchase;

            // _context.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(stock.ToStockDto());

        }

        [HttpDelete]
        [Route("DeleteStock/{id}")]
        public async Task<ActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}