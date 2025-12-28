using Azure.Core;

namespace FileManager.API.Contracts;

public class UploadImageRequestValidator : AbstractValidator<UploadImageRequest>
{
    public UploadImageRequestValidator()
    {
        RuleFor(x => x.Image)
            .SetValidator(new BlockedSignaturesValidator())
            .SetValidator(new FileSizeValidator());

        RuleFor(x => x.Image)
            .Must((request, context) =>
            {
                var extension = Path.GetExtension(request.Image.FileName.ToLower());
                var isAllowed = FileSettings.AllowedImageExtensions.Contains(extension);

                return isAllowed;
            })
            .WithMessage("Image Extension is not allowed")
            .When(x => x.Image is not null);
    }
}
