using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.FileFacades.Abstractions;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Controllers;

[Route("api/pdf")]
public sealed class PdfController : BaseController
{
    private readonly IPdfService _pdfService;

    public PdfController(IPdfService pdfService)
    {
        _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
    }

    [HttpGet("{**link}")]
    public async Task<IActionResult> Get(string link)
    {
        IValidationResult validationResult = _pdfService.ValidateLink(link);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        bool fileExists = await _pdfService.ExistsAsync(link);

        if (!fileExists)
            return NotFound();

        return await _pdfService.GetAsync(link);
    }

    [HttpPost]
    public Task<FileLinkDto> Post(IFormFile file) => _pdfService.CreateAsync(file);
}
