using api.DTO.Stock;
using api.Mapper;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController(IStockRepository repository) : ControllerBase
    {
        private readonly IStockRepository _repository = repository;

        [HttpGet]
        [Route("GetStocks")]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStocks()
        {
            var stocks = await _repository.GetAllStocks();
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet]
        [Route("GetStock/{id}")]
        public async Task<ActionResult<StockDto>> GetStock(int id)
        {
            var stock = await _repository.GetStockById(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock.ToStockDto();
        }

        [HttpPost]
        [Route("CreateStock")]
        public async Task<ActionResult<StockDto>> CreateStock([FromBody] RequestCreateStockDto stockDto)
        {
            var stock = await _repository.CreateStock(stockDto);
            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("UpdateStock/{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] RequestUpdateStockDto stockDto)
        {
            Stock? stock = await _repository.UpdateStock(id, stockDto);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());

        }

        [HttpDelete]
        [Route("DeleteStock/{id}")]
        public async Task<ActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _repository.DeleteStock(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}