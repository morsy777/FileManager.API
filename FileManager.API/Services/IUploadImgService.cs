namespace FileManager.API.Services;

public interface IUploadImgService
{
    Task UploadImage(IFormFile img);
    Task Upload(IFormFile image);
    Task<IEnumerable<string>> GetImages();
}
