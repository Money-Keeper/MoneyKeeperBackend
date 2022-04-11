namespace MoneyKeeper.Domain.AutoMapper;

public sealed class MapperException : Exception
{
    public MapperException(Type sourceType, Type targetType)
        : base($"Mapper wasnt created\nSource type: {sourceType.FullName}\nTarget type: {targetType.FullName}")
    {
    }
}
