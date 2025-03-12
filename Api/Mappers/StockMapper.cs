using Api.Dtos.StockDtos;
using Api.Entities;

namespace Api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new()
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStock(this CreateStockDto createStock)
        {
            return new ()
            {
                Symbol = createStock.Symbol,
                CompanyName = createStock.CompanyName,
                Purchase = createStock.Purchase,
                LastDiv = createStock.LastDiv,
                Industry = createStock.Industry,
                MarketCap = createStock.MarketCap
            };
        }
    }
}
