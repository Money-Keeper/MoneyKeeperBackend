namespace MoneyKeeper.Domain.Mappers;

public interface IMapperConfiguration
{
    IReadOnlyDictionary<string, IEnumerable<PropertyMap>> Maps { get; }

    string GetMapName(Type sourceType, Type targetType);
    void CreateMapper<TSource, TTarget>()
        where TSource : class, new()
        where TTarget : class, new();
}
