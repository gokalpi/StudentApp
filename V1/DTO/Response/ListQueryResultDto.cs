using System.Collections.Generic;

namespace StudentApp.V1.DTO.Response
{
    public class ListQueryResultDto<T>
    {
        public bool Success { get; }
        public List<string> Messages { get; }
        public int TotalCount { get; } = 0;
        public IList<T> Items { get; } = new List<T>();

        public ListQueryResultDto(IList<T> items)
        {
            Success = true;
            Messages = new List<string>();
            TotalCount = items.Count;
            Items = items;
        }

        public ListQueryResultDto(List<string> messages)
        {
            Success = false;
            Messages = messages ?? new List<string>();
            TotalCount = 0;
            Items = default;
        }

        public ListQueryResultDto(string message)
        {
            Success = false;
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }
            TotalCount = 0;
            Items = default;
        }
    }
}