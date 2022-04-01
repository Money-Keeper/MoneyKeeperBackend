namespace MoneyKeeper.Domain.Data.Models;

public class DataResult<TDto> where TDto : class
{
    public DataResult(IEnumerable<TDto> dtos)
    {
        Data = dtos ?? throw new ArgumentNullException(nameof(dtos));
    }

    public IEnumerable<TDto> Data { get; }
}
