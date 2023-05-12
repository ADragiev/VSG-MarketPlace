using Application.Models.ImageModels.Dtos;
using FluentValidation;

namespace Application.Helpers.Validators
{
    public class ImageCreateValidator : AbstractValidator<ImageCreateDto>
    {
        public ImageCreateValidator()
        {
            int fiveMegabytesInLength = 1024 * 1024 * 5;
            RuleFor(x => x.Image)
            .Custom((value, context) => { if (value == null || value.Length == 0) { context.AddFailure("Image cannot be empty!"); } })
            .Custom((value, context) => { if (value != null && value.Length > fiveMegabytesInLength) { context.AddFailure("File cannot be more than five megabytes!"); } })
            .Custom((value, context) => { if (value != null && !value.ContentType.StartsWith("image/")) { context.AddFailure("File must be an image!"); } });
        }
    }
}
