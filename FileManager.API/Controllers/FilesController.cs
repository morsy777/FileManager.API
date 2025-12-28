using FileManager.API.Contracts;
using FileManager.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController(IUploadImgService uploadImgService, IFileService fileService) : ControllerBase
{
    private readonly IUploadImgService _uploadImgService = uploadImgService;
    private readonly IFileService _fileService = fileService;

    [HttpPost("upload-profile-image")]
    public async Task<IActionResult> UploadProfilePhoto(IFormFile image)
    {
        if(image is not null && image!.Length != 0)
        {
            await _uploadImgService.Upload(image);
            return NoContent();
        }

        return BadRequest();
    }

    [HttpGet("get-image")]
    public async Task<IActionResult> GetProfilePhoto()
    {
        var result = await _uploadImgService.GetImages();
        return result is null ? BadRequest() : Ok(result);
    }

    [HttpPost("upload-file")]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request, CancellationToken cancellationToken)
    {
        var fileId = await _fileService.UploadAsync(request.File, cancellationToken);
        return CreatedAtAction(nameof(Stream), new {id = fileId}, null);
    }

    [HttpPost("upload-many")]
    public async Task<IActionResult> UploadMany([FromForm] UploadManyFilesRequest request, CancellationToken cancellationToken)
    {
        var fielsIds = await _fileService.UploadManyAsync(request.Files, cancellationToken);
        return Ok(new {fielsIds});
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request, CancellationToken cancellationToken)
    {
        await _fileService.UploadImageAsync(request.Image, cancellationToken);
        return Created();
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _fileService.DownloadAsync(id, cancellationToken);

        if(result is null)
            return NotFound();

        return File(result.Value.fileContent, result.Value.contentType, result.Value.fileName);
    }

    [HttpGet("stream/{id}")]
    public async Task<IActionResult> Stream([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _fileService.StreamAsync(id, cancellationToken);

        if(result is null)
            return NotFound();

        return File(result.Value.stream, result.Value.contentType, result.Value.fileName, enableRangeProcessing: true);
    }
}
