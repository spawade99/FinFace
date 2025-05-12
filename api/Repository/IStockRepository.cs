using api.DTO.Stock;
using api.Models;

namespace api.Repository
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStocks();
        Task<Stock?> GetStockById(int id);
        Task<Stock> CreateStock(RequestCreateStockDto createStockDto);
        Task<Stock?> UpdateStock(int id, RequestUpdateStockDto updateStockDto);
        Task<Stock?> DeleteStock(int id);
    }
}