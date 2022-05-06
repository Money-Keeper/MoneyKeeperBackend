using MoneyKeeper.Domain.Tools.Abstractions;

namespace MoneyKeeper.Domain.Tools;

public sealed class PathConverter : IPathConverter
{
    public string FromLink(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentNullException(nameof(link));

        return link.Replace('/', '\\');
    }

    public string ToLink(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        return path.Replace('\\', '/');
    }
}
