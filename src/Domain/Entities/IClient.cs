using System;

namespace Doctrina.Domain.Entities
{
    public interface IClient
    {
        string API { get; set; }
        string Authority { get; set; }
        long ClientId { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string[] Scopes { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}