using Application.Models.LentItemModels.Dtos;
using Application.Models.OrderModels.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Validators
{
    public class LentItemCreateValidator : AbstractValidator<LentItemCreateDto>
    {
        public LentItemCreateValidator()
        {
            RuleFor(l => l.Qty).NotNull().WithMessage("Lend qty quantity is required!")
               .GreaterThan(0).WithMessage("Order quantity must be a positive number!");

            RuleFor(l => l.LentBy).NotNull().WithMessage("Lent by cannot be empty");
        }
    }
}
