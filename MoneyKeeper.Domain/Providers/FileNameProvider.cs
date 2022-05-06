using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Domain.Providers;

public sealed class FileNameProvider : IFileNameProvider
{
    public string GetNewFileName()
    {
        return Guid.NewGuid().ToString();
    }
}
