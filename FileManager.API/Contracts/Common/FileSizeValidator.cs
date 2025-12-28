namespace FileManager.API.Contracts.Common;

public class FileSizeValidator : AbstractValidator<IFormFile>
{
    public FileSizeValidator()
    {
        RuleFor(x => x)
            .Must((request, context) => request.Length <= FileSettings.MaxFileSizeInBytes)
            .WithMessage($"Max File Size is {FileSettings.MaxFileSizeInMB} MB")
            .When(x => x is not null);
    }
}
