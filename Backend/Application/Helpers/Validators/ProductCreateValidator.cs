using Application.Models.CategoryModels.Contacts;
using Application.Models.LocationModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using FluentValidation;

namespace Application.Helpers.Validators
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly ILocationRepository locationRepo;

        public ProductCreateValidator(ICategoryRepository categoryRepo
            , ILocationRepository locationRepo)
        {
            this.categoryRepo = categoryRepo;
            this.locationRepo = locationRepo;

            RuleFor(p => p.Code).NotEmpty().NotNull().WithMessage("Product code is required!");
            RuleFor(p => p.Name).MaximumLength(50).NotNull().WithMessage("Product code is required!").NotEmpty().WithMessage("Product name is required");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be positive number");
            RuleFor(p => p.SaleQty).GreaterThanOrEqualTo(0).WithMessage("Sale quantity must be positive number");
            RuleFor(p => p.Description).MaximumLength(200).WithMessage("Description length cannot be over 200 characters!");
            RuleFor(p => p.CombinedQty).GreaterThanOrEqualTo(p => p.SaleQty).WithMessage("Combined quantity must be greater than or equal to sale quantity");
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
