namespace MoneyKeeper.Domain.AutoMapper;

public interface IMapperConfiguration
{
    ObjectMapConfiguration<TSource, TTarget> CreateMap<TSource, TTarget>()
        where TSource : class, new()
        where TTarget : class, new();

    Func<TSource, TTarget> GetMapper<TSource, TTarget>()
        where TSource : class, new()
        where TTarget : class, new();
}
