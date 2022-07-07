namespace MoneyKeeper.Validation.Abstractions;

public interface IValidationResult
{
    bool IsFailed { get; }
    IReadOnlyDictionary<string, string> Errors { get; }
}
