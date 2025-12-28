namespace FileManager.API.Contracts;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
        //RuleFor(x => x.File)
        //    .Must((request, context) => request.File.Length <= FileSettings.MaxFileSizeInBytes)
        //    .WithMessage($"Max File Size is {FileSettings.MaxFileSizeInMB} MB")
        //    .When(x => x.File is not null);

        //RuleFor(x => x.File)
        //    .SetValidator(new FileSizeValidator());

        RuleFor(x => x.File)
            .Must((request, context) =>
            {
                BinaryReader binary = new(request.File.OpenReadStream());
                var bytes = binary.ReadBytes(2);

                var fileSequenceHex = BitConverter.ToString(bytes); 

                foreach (var signature in FileSettings.BlockedSignature)
                    if (signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
                        return false;

                return true;
            })
            .WithMessage("Not Allowed File Content")
            .When(x => x.File is not null);

        RuleFor(x => x.File.Name)
            .Must(name => !name.Any(c => FileSettings.BlockedCharsInFileName.Contains(c)))
            .WithMessage("Invalid File Name")
            .When(x => x.File is not null);
    }
}

// Must((request, context)
// request represent an instance from the class/record that we make validation on it
// context is an addational parameters provided by FluentValidation to give us additional info about
// the attribute that we make validation on it, in our example: File attribute.
