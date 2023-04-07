using Application.Models.ProductModels.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Validators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdatetDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(p => p.FullName).NotNull().NotEmpty().WithMessage("Product name cannot be empty or null!");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be positive number");
            RuleFor(p => p.SaleQty).GreaterThanOrEqualTo(0).WithMessage("Sale quantity must be positive number");
            RuleFor(p => p.CombinedQty).GreaterThanOrEqualTo(p => p.SaleQty).WithMessage("Combined quantity must be greater than or equal to sale quantity");
        }
    }
}
