using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Domain.Dtos;
using MoneyKeeper.Domain.Providers.FilesProvider;
using MoneyKeeper.Domain.Tools.Abstractions;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class PdfController : ControllerBase
{
    private readonly IFilesProvider _filesProvider;
    private readonly IPathConverter _pathConverter;

    public PdfController(IFilesProvider filesProvider, IPathConverter pathConverter)
    {
        _filesProvider = filesProvider ?? throw new ArgumentNullException(nameof(filesProvider));
        _pathConverter = pathConverter ?? throw new ArgumentNullException(nameof(pathConverter));
    }

    [HttpGet("{**filePath}")]
    public async Task<IActionResult> Get(string filePath)
    {
        bool isPdf = filePath.EndsWith(FileExtensions.Pdf);

        if (!isPdf)
            return BadRequest();

        filePath = _pathConverter.FromUrl(filePath);

        bool fileExists = _filesProvider.Exists(filePath, FileType.Pdf);

        if (!fileExists)
            return NotFound();

        byte[] pdf = await _filesProvider.GetAsync(filePath, FileType.Pdf);

        return File(pdf, MediaTypeNames.Application.Pdf);
    }

    [HttpPost]
    public async Task<ActionResult<FilePathDto>> Post(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();

        byte[] pdf = new byte[stream.Length];

        await stream.ReadAsync(pdf);

        string path = await _filesProvider.SaveAsync(pdf, FileType.Pdf);

        return new FilePathDto(_pathConverter.ToUrl(path));
    }
}
