namespace FileManager.API.Contracts;

public record UploadImageRequest(
    IFormFile Image
);