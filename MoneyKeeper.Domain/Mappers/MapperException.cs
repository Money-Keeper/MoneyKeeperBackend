namespace MoneyKeeper.Domain.Mappers;

public class MapperException : Exception
{
    public MapperException(Type sourceType, Type targetType)
        : base($"Mapper wasnt added\nSource type: {sourceType.FullName}\nTarget type: {targetType.FullName}")
    {
    }
}
