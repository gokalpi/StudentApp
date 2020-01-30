using System.Collections.Generic;

namespace StudentApp.V1.DTO.Response
{
    public class QueryResultDto<T>
    {
        public bool Success { get; }
        public List<string> Messages { get; }
        public T Resource { get; }

        public QueryResultDto(T resource)
        {
            Success = true;
            Messages = new List<string>();
            Resource = resource;
        }

        public QueryResultDto(List<string> messages)
        {
            Success = false;
            Messages = messages ?? new List<string>();
            Resource = default;
        }

        public QueryResultDto(string message)
        {
            Success = false;
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }

            Resource = default;
        }
    }
}