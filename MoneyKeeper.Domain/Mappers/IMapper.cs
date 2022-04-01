namespace MoneyKeeper.Domain.Mappers;

public interface IMapper
{
    TTarget? Map<TSource, TTarget>(TSource? source)
        where TSource : class, new()
        where TTarget : class, new();

    IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> source)
        where TSource : class, new()
        where TTarget : class, new();
}
