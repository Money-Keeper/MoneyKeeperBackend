namespace MoneyKeeper.Domain.Mappers;

public class MapperConfiguration : IMapperConfiguration
{
    private readonly Dictionary<string, IEnumerable<PropertyMap>> _maps = new Dictionary<string, IEnumerable<PropertyMap>>();

    public IReadOnlyDictionary<string, IEnumerable<PropertyMap>> Maps => _maps;

    public void CreateMapper<TSource, TTarget>()
        where TSource : class, new()
        where TTarget : class, new()
    {
        Type sourceType = typeof(TSource);
        Type targetType = typeof(TTarget);

        string mapName = GetMapName(sourceType, targetType);

        if (_maps.ContainsKey(mapName))
            return;

        var map = GetMatchingProperties(sourceType, targetType);

        _maps.Add(mapName, map);
    }

    public string GetMapName(Type sourceType, Type targetType)
    {
        return sourceType.FullName + '_' + targetType.FullName;
    }

    private IEnumerable<PropertyMap> GetMatchingProperties(Type sourceType, Type targetType)
    {
        return from s in sourceType.GetProperties()
               from t in targetType.GetProperties()
               where s.Name == t.Name
               && s.CanRead
               && t.CanWrite
               && IsSameType(s.PropertyType, t.PropertyType)
               select new PropertyMap(s, t);
    }

    private bool IsSameType(Type sourceType, Type targetType)
    {
        return sourceType == targetType
            || Nullable.GetUnderlyingType(sourceType) == targetType
            || sourceType == Nullable.GetUnderlyingType(targetType);
    }
}
