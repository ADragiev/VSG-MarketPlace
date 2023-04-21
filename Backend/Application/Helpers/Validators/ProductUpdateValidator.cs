using Application.Models.CategoryModels.Contacts;
using Application.Models.LocationModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Validators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly ILocationRepository locationRepo;

        public ProductUpdateValidator(ICategoryRepository categoryRepo,
            ILocationRepository locationRepo)
        {
            this.categoryRepo = categoryRepo;
            this.locationRepo = locationRepo;

            RuleFor(p => p.Code).NotEmpty().WithMessage("Product code is required!").NotNull().WithMessage("Product code is required!");
            RuleFor(p => p.Name).MaximumLength(50).NotNull().WithMessage("Product code is required!").NotEmpty().WithMessage("Product name is required");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be positive number");
            RuleFor(p => p.SaleQty).GreaterThanOrEqualTo(0).WithMessage("Sale quantity must be positive number");
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
