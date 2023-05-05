using Application.Models.OrderModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Validators
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator()
        {
            RuleFor(o => o.Qty).NotNull().WithMessage("Order quantity is required!")
                .GreaterThan(0).WithMessage("Order quantity must be a positive number!");
        }
    }
}
