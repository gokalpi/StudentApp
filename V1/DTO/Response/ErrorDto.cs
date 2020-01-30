using System.Collections.Generic;

namespace StudentApp.V1.DTO.Response
{
    public class ErrorDto
    {
        public bool Success => false;
        public List<string> Messages { get; private set; }

        public ErrorDto(List<string> messages)
        {
            Messages = messages ?? new List<string>();
        }

        public ErrorDto(string message)
        {
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }
        }
    }
}