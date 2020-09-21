using Doctrina.Domain.Entities;
using System;

namespace Doctrina.Application.Common.Models
{
    public class ClientAuthenticationResult
    {
        public Client Client { get; private set; }
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public Exception Exception { get; private set; }

        public static ClientAuthenticationResult Fail(string message, Exception exception = default)
        {
            return new ClientAuthenticationResult()
            {
                IsSuccess = false,
                Message = message,
                Exception = exception
            };
        }

        public static ClientAuthenticationResult Success(Client client)
        {
            return new ClientAuthenticationResult()
            {
                Client = client,
                IsSuccess = true,
                Exception = null,
                Message = string.Empty
            };
        }
    }
}
