namespace Api.Helpers
{
    public class StockQuery
    {
        public string? Symbol { get; set; } = string.Empty;
        public string? CompanyName { get; set; } = string.Empty;
        public string? SortBy { get; set; } = string.Empty;
        public bool Decend { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;


        public void Deconstruct (
            out string? symbol, 
            out string? companyName,
            out string? sortBy,
            out bool decend,
            out int pageNumber,
            out int pageSize)
        {
            symbol = Symbol;
            companyName = CompanyName;
            sortBy = SortBy;
            decend = Decend;
            pageNumber = PageNumber;
            pageSize = PageSize;
        }
    }
}
