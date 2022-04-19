namespace MoneyKeeper.Domain.Tools.Abstractions;

public interface IPathConverter
{
    string FromUrl(string url);
    string ToUrl(string path);
}
