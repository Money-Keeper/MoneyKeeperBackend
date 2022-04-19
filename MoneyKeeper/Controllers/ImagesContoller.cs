using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Domain.Dtos;
using MoneyKeeper.Domain.Providers.FilesProvider;
using MoneyKeeper.Domain.Tools.Abstractions;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class ImagesController : ControllerBase
{
    private readonly IFilesProvider _filesProvider;
    private readonly IPathConverter _pathConverter;

    public ImagesController(IFilesProvider filesProvider, IPathConverter pathConverter)
    {
        _filesProvider = filesProvider ?? throw new ArgumentNullException(nameof(filesProvider));
        _pathConverter = pathConverter ?? throw new ArgumentNullException(nameof(pathConverter));
    }

    [HttpGet("{**filePath}")]
    public async Task<IActionResult> Get(string filePath)
    {
        bool isImage = filePath.EndsWith(FileExtensions.Jpg) || filePath.EndsWith(FileExtensions.Jpeg);

        if (!isImage)
            return BadRequest();

        filePath = _pathConverter.FromUrl(filePath);

        bool fileExists = _filesProvider.Exists(filePath, FileType.Image);
        
        if (!fileExists)
            return NotFound();

        byte[] image = await _filesProvider.GetAsync(filePath, FileType.Image);

        return File(image, MediaTypeNames.Image.Jpeg);
    }

    [HttpPost]
    public async Task<ActionResult<FilePathDto>> Post(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();

        byte[] image = new byte[stream.Length];

        await stream.ReadAsync(image);

        string path = await _filesProvider.SaveAsync(image, FileType.Image);

        return new FilePathDto(_pathConverter.ToUrl(path));
    }
}
