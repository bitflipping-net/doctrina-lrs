using AutoMapper;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class StatementTypeConverter : ITypeConverter<StatementModel, Statement>
    {
        public Statement Convert(StatementModel source, Statement destination, ResolutionContext context)
        {
            // FullStatement is the fastest way, allows us to not innerjoin other tables for the final result.
            // But it does require fullStatement to be update to date.
            var stmt = new Statement(source.Encoded.Payload);
            if (stmt.Attachments != null && stmt.Attachments.Count > 0)
            {
                context.Mapper.Map(source.Attachments, stmt.Attachments);
            }
            return stmt;
        }
    }
}
