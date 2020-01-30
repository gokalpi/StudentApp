namespace StudentApp.V1.Domain.Models.Queries
{
    public class Query
    {
        private const int DEFAULT_PAGE_SIZE = 10;

        public int Page { get; protected set; }
        public int PageSize { get; protected set; }

        public Query(int page, int itemsPerPage)
        {
            Page = page <= 0 ? 1 : page;
            PageSize = itemsPerPage <= 0 ? DEFAULT_PAGE_SIZE : itemsPerPage;
        }
    }
}