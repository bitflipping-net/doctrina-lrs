using AutoMapper;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Infrastructure.Automapper.Mappings.TypeConverters
{
    public class IriTypeConverter : ITypeConverter<string, Iri>
    {
        public Iri Convert(string source, Iri destination, ResolutionContext context)
        {
            if (Iri.TryParse(source, out Iri iri))
            {
                return iri;
            }

            return null;
        }
    }
}