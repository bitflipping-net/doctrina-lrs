using System;
using AutoMapper;
using Doctrina.Persistence;
using Xunit;

namespace Doctrina.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public DoctrinaDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = DoctrinaContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            DoctrinaContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}