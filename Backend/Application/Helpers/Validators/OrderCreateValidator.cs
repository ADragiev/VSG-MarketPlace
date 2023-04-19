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
        private readonly IProductRepository productRepo;

        public OrderCreateValidator(IProductRepository productRepo)
        {
            this.productRepo = productRepo;

            RuleFor(o => o.OrderedBy).NotEmpty().NotNull();
            RuleFor(o => o.Qty).NotEmpty().WithMessage("Order quantity is required!")
                .NotNull().WithMessage("Order quantity is required!")
                .GreaterThan(0).WithMessage("Order quantity must be a positive number!");
            RuleFor(o => o.ProductId).MustAsync(async (productId, cancellation) =>
            {
                var product = await productRepo.GetByID(productId);
                return product != null;
            }).WithMessage("Product Id is not valid!");
            RuleFor(o => o).MustAsync(async (order, cancellation) =>
            {
                var product = await productRepo.GetByID(order.ProductId);
                if (product != null)
                {
                    return product.SaleQty >= order.Qty;
                }
                return true;
            }).WithMessage("Not enough quantity!");
        }
    }
}
