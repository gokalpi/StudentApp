using System.Collections.Generic;

namespace StudentApp.V1.Domain.Models.Queries
{
    public class QueryResult<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}