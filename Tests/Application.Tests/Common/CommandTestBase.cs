using AutoMapper;
using Doctrina.Persistence;
using System;

namespace Doctrina.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly DoctrinaDbContext _context;
        protected readonly StoreDbContext _storeContext;
        protected readonly IMapper _mapper;

        public CommandTestBase()
        {
            _context = DoctrinaContextFactory.Create();
            _storeContext = DoctrinaContextFactory.CreateStoreContext(_context);
            _mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            DoctrinaContextFactory.Destroy(_context);
        }
    }
}
