using api.Data;
using api.DTO.Stock;
using api.Models;
using api.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Formats.Asn1;

namespace api.Repository
{
    public class StockRepository(ApplicationDBContext dBContext) : IStockRepository
    {
        private readonly ApplicationDBContext _dBContext = dBContext;

        public async Task<Stock> CreateStock(RequestCreateStockDto createStockDto)
        {
            Stock stock = createStockDto.ToStockModelFromCreateStockDto();
            _dBContext.Stocks.Add(stock);
            await _dBContext.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStock(int id)
        {
            var stock = await _dBContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            _dBContext.Stocks.Remove(stock);
            await _dBContext.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> IsStockExists(int id)
        {
            return await _dBContext.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            return await _dBContext.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetStockById(int id)
        {
            return await _dBContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateStock(int id, RequestUpdateStockDto updateStockDto)
        {
            Stock? stock = await _dBContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            stock.Symbol = updateStockDto.Symbol;
            stock.CompanyName = updateStockDto.CompanyName;
            stock.Industry = updateStockDto.Industry;
            stock.LastDiv = updateStockDto.LastDiv;
            stock.MarketCap = updateStockDto.MarketCap;
            stock.Purchase = updateStockDto.Purchase;

            _dBContext.Stocks.Update(stock);
            await _dBContext.SaveChangesAsync();
            return stock;
        }
    }
}