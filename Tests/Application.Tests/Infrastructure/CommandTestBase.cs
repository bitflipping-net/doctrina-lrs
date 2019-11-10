using Doctrina.Persistence;
using System;

namespace Doctrina.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly DoctrinaDbContext _context;

        public CommandTestBase()
        {
            _context = DoctrinaContextFactory.Create();
        }

        public void Dispose()
        {
            DoctrinaContextFactory.Destroy(_context);
        }
    }
}
