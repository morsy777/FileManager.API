namespace FileManager.API.Contracts;

public class UploadManyFilesRequestValidator : AbstractValidator<UploadManyFilesRequest>
{
    public UploadManyFilesRequestValidator()
    {
        RuleForEach(x => x.Files)
            .SetValidator(new FileSizeValidator());

        RuleForEach(x => x.Files)
            .SetValidator(new BlockedSignaturesValidator());

        RuleForEach(x => x.Files)
            .SetValidator(new FileNameValidator());

    }
}
