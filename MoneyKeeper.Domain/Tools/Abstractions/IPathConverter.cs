namespace MoneyKeeper.Domain.Tools.Abstractions;

public interface IPathConverter
{
    string FromLink(string link);
    string ToLink(string path);
}
