using Application.Models.ImageModels.Dtos;
using FluentValidation;

namespace Application.Helpers.Validators
{
    public class ImageCreateValidator : AbstractValidator<ImageCreateDto>
    {
        public ImageCreateValidator()
        {
            RuleFor(i=>i.Image).NotNull().WithMessage("Image is required");
        }
    }
}
