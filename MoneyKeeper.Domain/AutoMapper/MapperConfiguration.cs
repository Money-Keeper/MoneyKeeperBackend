namespace MoneyKeeper.Domain.AutoMapper;

public sealed class MapperConfiguration : IMapperConfiguration
{
    private readonly IDictionary<string, ObjectMap> _maps = new Dictionary<string, ObjectMap>();

    public MapperConfiguration(params Func<IMapperConfiguration, IObjectMapConfiguration>[] configuration)
    {
        CreateMaps(configuration.Select(x => x(this)));
    }

    public ObjectMapConfiguration<TSource, TTarget> CreateMap<TSource, TTarget>()
        where TSource : class, new()
        where TTarget : class, new()
    {
        return new ObjectMapConfiguration<TSource, TTarget>();
    }

    private void CreateMaps(IEnumerable<IObjectMapConfiguration> mapConfigurations)
    {
        foreach (IObjectMapConfiguration mapConfig in mapConfigurations)
        {
            string mapName = GetMapName(mapConfig.SourceType, mapConfig.TargetType);
            _maps.Add(mapName, new ObjectMap(mapConfig.PropertyMaps));
        }
    }

    public Func<TSource?, TTarget?> GetMapper<TSource, TTarget>()
        where TSource : class, new()
        where TTarget : class, new()
    {
        Func<object?, object?> mapper = GetMapperFunc(typeof(TSource), typeof(TTarget));
        // TODO: Maybe should be improved sometime.
        return source => (TTarget?)mapper(source);
    }

    private Func<object?, object?> GetMapperFunc(Type sourceType, Type targetType)
    {
        string mapName = GetMapName(sourceType, targetType);

        if (!_maps.ContainsKey(mapName))
            throw new MapperException(sourceType, targetType);

        ObjectMap objectMap = _maps[mapName];

        return source =>
        {
            if (source is null)
                return null;

            object targetObj = Activator.CreateInstance(targetType)!;

            foreach (PropertyMap propertyMap in objectMap.PropertyMaps)
            {
                object? sourceValue = propertyMap.SourceProperty.GetValue(source);

                if (propertyMap.IsNestedEntity)
                {
                    Func<object?, object?> mapper = GetMapperFunc(
                        sourceType: propertyMap.SourceProperty.PropertyType,
                        targetType: propertyMap.TargetProperty.PropertyType);

                    propertyMap.TargetProperty.SetValue(targetObj, mapper(sourceValue));

                    continue;
                }

                propertyMap.TargetProperty.SetValue(targetObj, sourceValue);
            }

            return targetObj;
        };
    }

    private string GetMapName(Type sourceType, Type targetType)
    {
        return sourceType.FullName + '_' + targetType.FullName;
    }
}
