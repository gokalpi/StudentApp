using System.Collections.Generic;

namespace StudentApp.V1.DTO.Response
{
    public class PagedQueryResultDto<T>
    {
        public bool Success { get; }
        public List<string> Messages { get; }
        public int PageIndex { get; } = 0;
        public int PageSize { get; } = 0;
        public int TotalCount { get; } = 0;
        public int TotalPages { get; } = 0;
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
        public IEnumerable<T> Items { get; } = new List<T>();

        public PagedQueryResultDto(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount, int totalPages)
        {
            Success = true;
            Messages = new List<string>();
            PageIndex = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize < 1 ? 10 : pageSize;
            TotalCount = totalCount < 0 ? 0 : totalCount;
            TotalPages = totalPages < 0 ? 0 : totalPages;
            Items = items;
        }

        public PagedQueryResultDto(List<string> messages)
        {
            Success = false;
            Messages = messages ?? new List<string>();
            Items = default;
        }

        public PagedQueryResultDto(string message)
        {
            Success = false;
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }
            Items = default;
        }
    }
}