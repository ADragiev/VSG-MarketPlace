using Application.Models.CategoryModels.Contacts;
using Application.Models.ProductModels.Dtos;
using FluentValidation;

namespace Application.Helpers.Validators
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        private readonly ICategoryRepository categoryRepo;

        public ProductCreateValidator(ICategoryRepository categoryRepo)
        {
            this.categoryRepo = categoryRepo;
            RuleFor(p => p.Code).NotEmpty().NotNull().WithMessage("Product code is required!");
            RuleFor(p => p.FullName).MaximumLength(50).NotNull().WithMessage("Product code is required!").NotEmpty().WithMessage("Product name is required");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be positive number");
            RuleFor(p => p.SaleQty).GreaterThanOrEqualTo(0).WithMessage("Sale quantity must be positive number");
            RuleFor(p => p.CombinedQty).GreaterThanOrEqualTo(p => p.SaleQty).WithMessage("Combined quantity must be greater than or equal to sale quantity");
            RuleFor(p => p.CategoryId).MustAsync(async (categoryId, cancellation) =>
            {
                var category = await categoryRepo.GetByID(categoryId);
                return category != null;
            }).WithMessage("Category Id is not valid!");
        }
    }
}
