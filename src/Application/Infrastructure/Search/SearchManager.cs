using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Infrastructure.Search
{
    public class SearchManager : ISearchManager
    {
        private readonly IDoctrinaDbContext _context;

        public ElasticLowLevelClient Client { get; }

        public SearchManager(IDoctrinaDbContext context)
        {
            _context = context;
            var settings = new ConnectionConfiguration(new Uri("http://localhost:9200"))
    .RequestTimeout(TimeSpan.FromMinutes(2));
            Client = new ElasticLowLevelClient(settings);
        }

        public async Task DeleteIndexesAsync(CancellationToken cancellationToken = default)
        {
            await Client.Indices.DeleteAsync<StringResponse>("statements", ctx: cancellationToken);
        }

        public async Task RebuildIndexesAsync(CancellationToken cancellationToken = default)
        {
            await DeleteIndexesAsync();

            int size = 5000;
            int pageIndex = 0;
            while (true)
            {
                var statements = await _context.Statements.AsNoTracking().Skip(pageIndex * size).Take(size).ToListAsync();
                if (statements != null && statements.Count() > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var statement in statements)
                    {
                        sb.AppendLine("{ \"index\": { \"_index\" :\"statements\", \"_id\": \"" + statement.StatementId.ToString() + "\" } }");
                        sb.AppendLine(statement.FullStatement);
                    }
                    var response = await Client.BulkAsync<StringResponse>(sb.ToString(), ctx: cancellationToken);
                    pageIndex++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
