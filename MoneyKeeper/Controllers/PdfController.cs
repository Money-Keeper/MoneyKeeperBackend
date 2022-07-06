using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.FileFacades;
using MoneyKeeper.Infrastructure.Attributes;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController, Route("api/pdf"), Authorize, Produces(MediaTypeNames.Application.Json)]
public sealed class PdfController : ControllerBase
{
    private readonly IPdfService _pdfService;

    public PdfController(IPdfService pdfService)
    {
        _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
    }

    [HttpGet("{**link}")]
    public async Task<IActionResult> Get(string link)
    {
        if (!_pdfService.IsValidLink(link))
            return BadRequest();

        bool fileExists = await _pdfService.ExistsAsync(link);

        if (!fileExists)
            return NotFound();

        return await _pdfService.GetAsync(link);
    }

    [HttpPost]
    public async Task<ActionResult<FileLinkDto>> Post(IFormFile file) => await _pdfService.CreateAsync(file);
}
