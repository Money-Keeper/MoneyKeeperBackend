namespace MoneyKeeper.Domain.Mappers;

public class Mapper : IMapper
{
    private readonly IMapperConfiguration _configuration;

    public Mapper(IMapperConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public TTarget? Map<TSource, TTarget>(TSource? source)
        where TSource : class, new()
        where TTarget : class, new()
    {
        if (source is null)
            return null;

        Type sourceType = typeof(TSource);
        Type targetType = typeof(TTarget);

        string mapName = _configuration.GetMapName(sourceType, targetType);

        if (!_configuration.Maps.ContainsKey(mapName))
            throw new MapperException(sourceType, targetType);

        TTarget target = Activator.CreateInstance<TTarget>();

        Copy(source, target, mapName);

        return target;
    }

    public IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> source)
        where TSource : class, new()
        where TTarget : class, new()
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        Type sourceType = typeof(TSource);
        Type targetType = typeof(TTarget);

        string mapName = _configuration.GetMapName(sourceType, targetType);

        if (!_configuration.Maps.ContainsKey(mapName))
            throw new MapperException(sourceType, targetType);

        IList<TTarget> result = new List<TTarget>(source.Count());

        foreach (TSource value in source)
        {
            TTarget target = Activator.CreateInstance<TTarget>();

            Copy(value, target, mapName);

            result.Add(target);
        }

        return result;
    }

    private void Copy(object source, object target, string mapName)
    {
        foreach (PropertyMap map in _configuration.Maps[mapName])
        {
            var sourceValue = map.SourceProperty.GetValue(source, null);
            map.TargetProperty.SetValue(target, sourceValue);
        }
    }
}
