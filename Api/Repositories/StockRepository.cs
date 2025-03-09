using System.Linq.Expressions;
using Api.Data;
using Api.Dtos.StockDtos;
using Api.Entities;
using Api.Helpers;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;
        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(StockQuery query)
        {
            var stocks = _context.Stocks.AsQueryable();

            var (symbol, companyName, sortBy, decend, pageNumber, pageSize) = query;

            // Filter
            if (!string.IsNullOrEmpty(symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(symbol));
            }

            // Filter
            if (!string.IsNullOrEmpty(companyName))
            {   
                stocks = stocks.Where(s => s.CompanyName.Contains(companyName));
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                if(HasProperty.Check<Stock>(sortBy, out string? propertyName))
                {
                    var param = Expression.Parameter(typeof(Stock), "s");
                    var property = Expression.Property(param, propertyName!);
                    var convert = Expression.Convert(property,typeof(object));
                    var lamda = Expression.Lambda<Func<Stock, object>>(convert, param);

                    stocks = decend ? stocks.OrderByDescending(lamda) : stocks.OrderBy(lamda);
                }
            }

            // Pagination
            var skipNumber = (pageNumber - 1) * pageSize;

            return await stocks.Skip(skipNumber).Take(pageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(
                s => string.Equals(
                    s.Symbol, 
                    symbol, 
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            _context.Stocks.Add(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stock)
        {
            var stockResult = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockResult is null) return stockResult;

            stockResult.Symbol = stock.Symbol;
            stockResult.CompanyName = stock.CompanyName;
            stockResult.Purchase = stock.Purchase;
            stockResult.LastDiv = stock.LastDiv;
            stockResult.Industry = stock.Industry;
            stockResult.MarketCap = stock.MarketCap;

            await _context.SaveChangesAsync();

            return stockResult;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockResult = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockResult is null) return stockResult;

            _context.Stocks.Remove(stockResult);
            await _context.SaveChangesAsync();

            return stockResult;
        }
    }
}
