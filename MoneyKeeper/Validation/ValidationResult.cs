using MoneyKeeper.Validation.Abstractions;
using System.Text.Json.Serialization;

namespace MoneyKeeper.Validation;

internal sealed class ValidationResult : IValidationResult
{
    private readonly Dictionary<string, string> _errors;

    public ValidationResult()
    {
        _errors = new Dictionary<string, string>();
    }

    [JsonIgnore]
    public bool IsFailed { get; private set; }
    public IReadOnlyDictionary<string, string> Errors => _errors;

    public void AddError(string key, string value)
    {
        lock (_errors)
        {
            IsFailed = true;
            _errors[key] = value;
        }
    }
}