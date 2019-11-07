using MediatR;
using Doctrina.Domain.Identity;
using System.Text;
using System;

namespace Doctrina.Application.Security.Queries
{

    public class GetUserByBasicAuth
    {

        public GetUserByBasicAuth(string basicAuth)
        {
            if(basicAuth.StartsWith("Basic"))
            {
                basicAuth = basicAuth.Substring(basicAuth.IndexOf("Basic")).Trim();
            }

            byte[] bytes = Convert.FromBase64String(basicAuth);
            string[] parts = Encoding.UTF8.GetString(bytes).Split(':');

            ApiUsername = parts[0];
            ApiPassword = parts[1];
        }

        public GetUserByBasicAuth(string apiUsername, string apiPassword)
        {
            ApiUsername = apiUsername;
            ApiPassword = apiPassword;
        }

        public string ApiUsername { get; }
        public string ApiPassword { get; }
    }
}