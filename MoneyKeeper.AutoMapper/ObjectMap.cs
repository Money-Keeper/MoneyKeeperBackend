namespace MoneyKeeper.AutoMapper;

public class ObjectMap
{
    public ObjectMap(IEnumerable<PropertyMap> propertyMaps)
    {
        PropertyMaps = propertyMaps ?? throw new ArgumentNullException(nameof(propertyMaps));
    }

    public IEnumerable<PropertyMap> PropertyMaps { get; }
}
