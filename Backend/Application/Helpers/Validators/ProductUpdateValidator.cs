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
            RuleFor(p => p.Code).NotEmpty().WithMessage("Product code is required!").NotNull().WithMessage("Product code is required!");
            RuleFor(p => p.FullName).NotNull().WithMessage("Product code is required!").NotEmpty().WithMessage("Product name is required");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be positive number");
            RuleFor(p => p.SaleQty).GreaterThanOrEqualTo(0).WithMessage("Sale quantity must be positive number");
            RuleFor(p => p.CombinedQty).GreaterThanOrEqualTo(p => p.SaleQty).WithMessage("Combined quantity must be greater than or equal to sale quantity");
        }
    }
}
