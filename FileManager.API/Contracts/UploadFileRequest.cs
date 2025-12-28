namespace FileManager.API.Contracts;

public record UploadFileRequest(
    IFormFile File
);