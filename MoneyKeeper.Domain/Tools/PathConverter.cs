using MoneyKeeper.Domain.Tools.Abstractions;

namespace MoneyKeeper.Domain.Tools;

public sealed class PathConverter : IPathConverter
{
    public string FromUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException(nameof(url));

        return url.Replace('/', '\\');
    }

    public string ToUrl(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        return path.Replace('\\', '/');
    }
}
