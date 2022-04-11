namespace MoneyKeeper.Domain.AutoMapper;

public class Mapper : IMapper
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
        if (source is null)
            return null;

        Func<TSource, TTarget> mapper = _mapperConfig.GetMapper<TSource, TTarget>();

        return mapper(source);
    }

    public IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> source)
        where TSource : class, new()
        where TTarget : class, new()
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        Func<TSource, TTarget> mapper = _mapperConfig.GetMapper<TSource, TTarget>();

        IList<TTarget> result = new List<TTarget>(source.Count());

        foreach (TSource value in source)
        {
            TTarget targetObj = mapper(value);
            result.Add(targetObj);
        }

        return result;
    }
}
