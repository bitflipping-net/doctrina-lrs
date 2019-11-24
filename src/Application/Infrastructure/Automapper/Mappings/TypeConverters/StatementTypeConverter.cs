using AutoMapper;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class StatementTypeConverter : ITypeConverter<IStatementEntity, Statement>
    {
        public Statement Convert(IStatementEntity source, Statement destination, ResolutionContext context)
        {
            // FullStatement is the fastest way, allows us to not innerjoin other tables for the final result.
            // But it does require fullStatement to be update to date.
            var stmt = new Statement(source.FullStatement);
            if(stmt.Attachments != null && stmt.Attachments.Count > 0)
            {
                context.Mapper.Map(source.Attachments, stmt.Attachments);
            }
            return stmt;
        }
    }
}
