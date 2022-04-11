namespace MoneyKeeper.Domain.AutoMapper;

public interface IObjectMapConfiguration
{
    Type SourceType { get; }
    Type TargetType { get; }
    IEnumerable<PropertyMap> PropertyMaps { get; }
}
