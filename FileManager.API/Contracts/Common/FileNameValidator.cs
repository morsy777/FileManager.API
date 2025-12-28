namespace FileManager.API.Contracts.Common;

public class FileNameValidator : AbstractValidator<IFormFile>
{
    public FileNameValidator()
    {
        RuleFor(x => x.Name)
            .Must(name => !name.Any(c => FileSettings.BlockedCharsInFileName.Contains(c)))
            .WithMessage("Invalid File Name")
            .When(x => x is not null);
    }
}
