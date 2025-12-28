namespace FileManager.API.Services;

public class UploadImgService(IWebHostEnvironment env, ApplicationDbContext db) : IUploadImgService
{
    private readonly IWebHostEnvironment _env = env;
    private readonly string _imgPath = $"{env.WebRootPath}/images";
    private readonly ApplicationDbContext _db = db;

    public async Task UploadImage(IFormFile image)
    {
        var uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");

        var uinqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var filePath = Path.Combine(uploadsFolderPath, uinqueFileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await image.CopyToAsync(stream);

        var relativePath = Path.Combine("uploads", uinqueFileName); // uploads/32432q-32qaf-agagra.png

        var img = new UploadedFile
        {
            FileName = image.FileName,
            StoredFileName = relativePath,
            ContentType = image.ContentType,
            FileExtension = Path.GetExtension(image.FileName)
        };

        await _db.Files.AddAsync(img);
        await _db.SaveChangesAsync();
    }

    // Optimize Upload Image
    public async Task Upload(IFormFile image)
    {
        var randomImageName = Guid.CreateVersion7().ToString() + Path.GetExtension(image.FileName);

        var path = Path.Combine(_imgPath, randomImageName);

        using var stream = File.Create(path);
        await image.CopyToAsync(stream);
    }

    public async Task<IEnumerable<string>> GetImages()
    {
        var images = await _db.Files
            .Select(x => x.StoredFileName) // StoredFileName = relative path
            .AsNoTracking()
            .ToListAsync();

        if(images is null || images.Count == 0)
            return Enumerable.Empty<string>();

        var baseUrl = "https://click2buy.runasp.net/";
        var fullUrls = images.Select(imgPath => baseUrl + imgPath.Replace("\\", "/"));

        return fullUrls;
    }
}
