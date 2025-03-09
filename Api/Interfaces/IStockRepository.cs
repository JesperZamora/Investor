using Api.Dtos.StockDtos;
using Api.Entities;
using Api.Helpers;

namespace Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(StockQuery query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockDto stock);
        Task<Stock?> DeleteAsync(int id);
    }
}
