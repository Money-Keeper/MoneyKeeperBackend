using System.Reflection;

namespace MoneyKeeper.AutoMapper;

public class PropertyMap
{
    public PropertyMap(PropertyInfo sourceProperty, PropertyInfo targetProperty, bool isNestedEntity = false)
    {
        SourceProperty = sourceProperty ?? throw new ArgumentNullException(nameof(sourceProperty));
        TargetProperty = targetProperty ?? throw new ArgumentNullException(nameof(targetProperty));
        IsNestedEntity = isNestedEntity;
    }

    public PropertyInfo SourceProperty { get; }
    public PropertyInfo TargetProperty { get; }
    public bool IsNestedEntity { get; }
}
