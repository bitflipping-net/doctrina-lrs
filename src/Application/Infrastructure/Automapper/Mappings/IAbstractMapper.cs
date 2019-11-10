namespace Doctrina.Application.Mappings
{
    public interface IMapFrom<TSource, TTarget>
    {
        TSource MapFrom(TTarget target, TSource source);
        TTarget MapFrom(TSource source, TTarget target);
    }
}