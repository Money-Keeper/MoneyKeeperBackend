namespace MoneyKeeper.AutoMapper.Abstractions;

public interface IObjectMapConfiguration
{
    Type SourceType { get; }
    Type TargetType { get; }
    IEnumerable<PropertyMap> PropertyMaps { get; }
}
