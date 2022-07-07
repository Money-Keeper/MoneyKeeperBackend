using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.FileFacades.Abstractions;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Controllers;

[Route("api/img")]
public sealed class ImagesController : BaseController
{
    private readonly IImagesService _imagesService;

    public ImagesController(IImagesService imageService)
    {
        _imagesService = imageService ?? throw new ArgumentNullException(nameof(imageService));
    }

    [HttpGet("{**link}")]
    public async Task<IActionResult> Get(string link)
    {
        IValidationResult validationResult = _imagesService.ValidateLink(link);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        bool fileExists = await _imagesService.ExistsAsync(link);

        if (!fileExists)
            return NotFound();

        return await _imagesService.GetAsync(link);
    }

    [HttpPost]
    public Task<FileLinkDto> Post(IFormFile file) => _imagesService.CreateAsync(file);
}
