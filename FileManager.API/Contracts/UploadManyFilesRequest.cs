namespace FileManager.API.Contracts;

public record UploadManyFilesRequest(
    IFormFileCollection Files
);
