using Application.Models.CategoryModels.Contacts;
using Application.Models.LocationModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using FluentValidation;

namespace Application.Helpers.Validators
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator(ICategoryRepository categoryRepo
            , ILocationRepository locationRepo)
        {
            RuleFor(p => p.Code)
                .MinimumLength(3).WithMessage("Product code length cannot be less than 3 characters!")
                .MaximumLength(50).WithMessage("Product code length cannot be over 50 characters!")
                .NotNull().WithMessage("Product code is required!")
                .NotEmpty().WithMessage("Product code is required!");

            RuleFor(p => p.Name)
                .NotNull().WithMessage("Product name is required")
                .NotEmpty().WithMessage("Product name is required")
                .MinimumLength(3).WithMessage("Product name length cannot be less than 3 characters!")
                .MaximumLength(100).WithMessage("Product name length cannot be over 100 characters!");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be positive number");

            RuleFor(p => p.SaleQty)
                .GreaterThanOrEqualTo(0).WithMessage("Sale quantity must be positive number");

            RuleFor(p => p.CombinedQty)
                .GreaterThanOrEqualTo(p => (p.LendQty != null ? p.LendQty : 0) + (p.SaleQty != null ? p.SaleQty : 0)).WithMessage("Combined quantity must be greater than or equal to sum of sale and lend quantity")
                .GreaterThanOrEqualTo(0).WithMessage("Combined quantity must be positive number");

            RuleFor(p => p.Description)
                .MaximumLength(200).WithMessage("Description length cannot be over 200 characters!");

            RuleFor(p => p.CategoryId).MustAsync(async (categoryId, cancellation) =>
            {
                var category = await categoryRepo.GetByIdAsync(categoryId);
                return category != null;
            }).WithMessage("Category Id is not valid!");

            RuleFor(p => p.LocationId).MustAsync(async (locationId, cancellation) =>
            {
                var location = await locationRepo.GetByIdAsync(locationId);
                return location != null;
            }).WithMessage("Location Id is not valid!");
        }
    }
}
