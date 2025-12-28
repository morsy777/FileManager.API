namespace FileManager.API.Settings;

public static class FileSettings
{
    public static int MaxFileSizeInMB { get; set; } = 1;
    public static int MaxFileSizeInBytes { get; set; } = MaxFileSizeInMB * 1024 * 1024;
    public static string[] BlockedSignature { get; set; } = ["D0-CF", "4D-5A", "2F-2A", "43-6F"];
    public static string[] AllowedImageExtensions { get; set; } = [".jpg", ".jpeg", ".png"];
    public static string BlockedCharsInFileName = "*\"/\\<>:|?";
}