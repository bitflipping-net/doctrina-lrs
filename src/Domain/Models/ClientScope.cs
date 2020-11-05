using System;

namespace Doctrina.Domain.Models
{
    public class ClientScope
    {
        public Guid ClientId { get; set; }
        public virtual Client Client {get;set;}
        public Guid ScopeId { get; set; }
        public virtual Scope Scope {get;set;}
    }
}
