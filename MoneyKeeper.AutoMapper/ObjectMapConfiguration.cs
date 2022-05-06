using MoneyKeeper.AutoMapper.Abstractions;
using System.Reflection;

namespace MoneyKeeper.AutoMapper;

public sealed class ObjectMapConfiguration<TSource, TTarget> : IObjectMapConfiguration
    where TSource : class, new()
    where TTarget : class, new()
{
    private readonly TSource _sourceObj = new TSource();
    private readonly TTarget _targetObj = new TTarget();

    private readonly IList<PropertyMap> _propertyMaps;

    public ObjectMapConfiguration()
    {
        _propertyMaps = new List<PropertyMap>(GetPropertyMaps(typeof(TSource), typeof(TTarget)));
    }

    public IEnumerable<PropertyMap> PropertyMaps => _propertyMaps;
    public Type SourceType { get; } = typeof(TSource);
    public Type TargetType { get; } = typeof(TTarget);

    public ObjectMapConfiguration<TSource, TTarget> AddCustomMap(Func<TSource, string> source, Func<TTarget, string> target)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        if (target is null)
            throw new ArgumentNullException(nameof(target));

        PropertyInfo sourceProperty = SourceType.GetProperty(source(_sourceObj))!;
        PropertyInfo targetProperty = TargetType.GetProperty(target(_targetObj))!;

        if (!sourceProperty.CanRead || !targetProperty.CanWrite)
            return this;

        bool isNestedEntity = !IsSameType(sourceProperty.PropertyType, targetProperty.PropertyType);

        PropertyMap map = new PropertyMap(sourceProperty, targetProperty, isNestedEntity);

        _propertyMaps.Add(map);

        return this;
    }

    private IEnumerable<PropertyMap> GetPropertyMaps(Type sourceType, Type targetType)
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
