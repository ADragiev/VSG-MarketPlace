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

        public List<CategoryGetDto> All()
        {
            return mapper.Map<List<CategoryGetDto>>(categoryRepo.GetAll());
        }

        public CategoryGetDto Create(CategoryCreateDto dto)
        {
            var category = mapper.Map<Category>(dto);

            int id = categoryRepo.Create(category);
            
            var createdCategory = categoryRepo.GetByID(id);
            return mapper.Map<CategoryGetDto>(createdCategory);
        }

        public void Delete(int id)
        {
            ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(id, categoryRepo);

            categoryRepo.Delete(id);
        }

        public CategoryGetDto GetById(int id)
        {
            ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(id, categoryRepo);
            var category = categoryRepo.GetByID(id);

            return mapper.Map<CategoryGetDto>(category);
        }

        public void Update(CategoryUpdateDto dto)
        {
            ThrowExceptionService.ThrowExceptionWhenIdNotFound<Category>(dto.CategoryId, categoryRepo);

            var categoryToUpdate = mapper.Map<Category>(dto);
            categoryRepo.Update(categoryToUpdate);
        }
    }
}
