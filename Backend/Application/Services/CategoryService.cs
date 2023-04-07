using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Models.GenericRepo;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> categoryRepo;
        private readonly IMapper mapper;

        public CategoryService(IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public async Task<List<CategoryGetDto>> All()
        {
            var categories = await categoryRepo.GetAll();
            return mapper.Map<List<CategoryGetDto>>(categories);
        }

        public async Task<CategoryGetDto> Create(CategoryCreateDto dto)
        {
            var category = mapper.Map<Category>(dto);

            int id = await categoryRepo.Create(category);
            
            var createdCategory = await categoryRepo.GetByID(id);
            return mapper.Map<CategoryGetDto>(createdCategory);
        }

        public async Task Delete(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(id, categoryRepo);

            await categoryRepo.Delete(id);
        }

        public async Task<CategoryGetDto> GetById(int id)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(id, categoryRepo);
            var category = await categoryRepo.GetByID(id);

            return mapper.Map<CategoryGetDto>(category);
        }

        public async Task Update(CategoryUpdateDto dto)
        {
            await ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(dto.CategoryId, categoryRepo);

            var categoryToUpdate = mapper.Map<Category>(dto);
            await categoryRepo.Update(categoryToUpdate);
        }
    }
}
