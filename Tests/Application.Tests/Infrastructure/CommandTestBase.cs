using AutoMapper;
using Doctrina.Persistence;
using System;

namespace Doctrina.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly DoctrinaDbContext _context;
        protected readonly IMapper _mapper;

        public CommandTestBase()
        {
            _context = DoctrinaContextFactory.Create();
            _mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            DoctrinaContextFactory.Destroy(_context);
        }
    }
}
