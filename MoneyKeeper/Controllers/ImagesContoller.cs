using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.FileFacades;
using MoneyKeeper.Infrastructure.Attributes;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController, Route("api/img"), Authorize, Produces(MediaTypeNames.Application.Json)]
public sealed class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageService)
    {
        _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
    }

    [HttpGet("{**link}")]
    public async Task<IActionResult> Get(string link)
    {
        if (!_imageService.IsValidLink(link))
            return BadRequest();

        bool fileExists = await _imageService.ExistsAsync(link);

        if (!fileExists)
            return NotFound();

        return await _imageService.GetAsync(link);
    }

    [HttpPost]
    public async Task<ActionResult<FileLinkDto>> Post(IFormFile file) => await _imageService.CreateAsync(file);
}
