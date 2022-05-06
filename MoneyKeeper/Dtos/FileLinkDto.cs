using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class FileLinkDto
{
    public FileLinkDto(string? fileLink)
    {
        FileLink = fileLink;
    }

    [Required, StringLength(256)]
    public string? FileLink { get; }
}
