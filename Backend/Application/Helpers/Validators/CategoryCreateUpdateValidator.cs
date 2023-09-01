using Application.Models.CategoryModels.Dtos;
using Application.Models.OrderModels.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Validators
{
    public class CategoryCreateUpdateValidator : AbstractValidator<CategoryCreateUpdateDto>
    {
        public CategoryCreateUpdateValidator()
        {
            RuleFor(o => o.Name).NotNull().WithMessage("Category name is required!")
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters!");
        }
    }
}
