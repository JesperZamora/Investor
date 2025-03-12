namespace Api.Helpers
{
    public class CommentQuery
    {
        public string? SortBy { get; set; }
        public bool Decend { get; set; } = false;

        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        
        public void Deconstruct(out string? sortBy, out bool decend, out int pageSize, out int pageNumber)
        {
            sortBy = SortBy;
            decend = Decend;
            pageSize = PageSize;
            pageNumber = PageNumber;
        }

    }
}
