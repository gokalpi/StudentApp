using System.Collections.Generic;

namespace StudentApp.V1.Domain.Models.Queries
{
    public class PagedQueryResult<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }

        public IEnumerable<T> Items { get; }
    }
}