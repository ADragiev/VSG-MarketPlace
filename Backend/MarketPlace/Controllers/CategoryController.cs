using Application.Helpers.Constants;
using Application.Helpers.Validators;
using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Models.ExceptionModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MarketPlace.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Policy = IdentityConstants.AdminRolePolicyName)]
        public async Task<List<CategoryGetDto>> GetAll()
        {
            return await categoryService.AllAsync();
        }
    }
}
