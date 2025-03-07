using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Seeds
{
    public static class SeedData
    {
        public static void GenerateSeed(this ModelBuilder builder)
        {
            List<Stock> stocks = 
            [
                new Stock() { Id = 1, Symbol = "TSLA", CompanyName = "Tesla", Purchase = 100m, LastDiv = 2m, Industry = "Automotive", MarketCap = 234234234 },
                new Stock() { Id = 2, Symbol = "MSFT", CompanyName = "Microsoft", Purchase = 100m, LastDiv = 1.2m, Industry = "Technology", MarketCap = 234234234 },
                new Stock() { Id = 3, Symbol = "VTI", CompanyName = "Vanguard Total Index", Purchase = 200m, LastDiv = 2.10m, Industry = "Index Fund", MarketCap = 234234234 },
                new Stock() { Id = 4, Symbol = "PLTR", CompanyName = "Plantir", Purchase = 23m, LastDiv = 0m, Industry = "Technology", MarketCap = 1234234 },
                new Stock() { Id = 5, Symbol = "AMZN", CompanyName = "Amazon", Purchase = 300m, LastDiv = 2m, Industry = "Technology", MarketCap = 234234234 },
            ];

            builder.Entity<Stock>().HasData(stocks);


            //List<Comment> comments = [];

            //builder.Entity<Stock>().HasData(comments);
        }
    }
}
