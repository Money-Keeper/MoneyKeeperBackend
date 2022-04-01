using System.Reflection;

namespace MoneyKeeper.Domain.Mappers;

public class PropertyMap
{
    public PropertyMap(PropertyInfo sourceProperty, PropertyInfo targetProperty)
    {
        SourceProperty = sourceProperty;
        TargetProperty = targetProperty;
    }

    public PropertyInfo SourceProperty { get; }
    public PropertyInfo TargetProperty { get; }
}
