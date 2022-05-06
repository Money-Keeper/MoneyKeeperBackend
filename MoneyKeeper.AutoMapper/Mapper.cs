using MoneyKeeper.AutoMapper.Abstractions;

namespace MoneyKeeper.AutoMapper;

public sealed class Mapper : IMapper
{
    private readonly IMapperConfiguration _mapperConfig;

    public Mapper(IMapperConfiguration mapperConfig)
    {
        _mapperConfig = mapperConfig ?? throw new ArgumentNullException(nameof(mapperConfig));
    }

    public TTarget? Map<TSource, TTarget>(TSource? source)
        where TSource : class, new()
        where TTarget : class, new()
    {
        Func<TSource?, TTarget?> mapper = _mapperConfig.GetMapper<TSource, TTarget>();

        return mapper(source);
    }

    public IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> source)
        where TSource : class, new()
        where TTarget : class, new()
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        if (!source.Any())
            return Enumerable.Empty<TTarget>();

        Func<TSource?, TTarget?> mapper = _mapperConfig.GetMapper<TSource, TTarget>();

        IEnumerable<TTarget> result = source.Where(x => x is not null).Select(x => mapper(x)!);

        return result;
    }
}
