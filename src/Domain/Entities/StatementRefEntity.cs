using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Domain.Entities
{
    public class StatementRefEntity : IStatementObjectEntity
    {
        public EntityObjectType ObjectType => EntityObjectType.StatementRef;
        public Guid StatementRefId { get; set; }
        public Guid? Id { get; set; }
    }
}
