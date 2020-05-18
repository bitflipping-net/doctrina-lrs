using System;

namespace Doctrina.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, object identifier)
            : base($"Entity \"{entity}\" ({identifier}) was not found.")
        {
        }
    }
}
