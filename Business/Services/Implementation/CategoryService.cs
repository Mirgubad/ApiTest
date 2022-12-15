using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Business.Services.Abstraction;
using Business.Validators.Category;
using Core.Entities;
using DataAccess.Repositories.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Response> CreateAsync(CategoryCreateDTO model)
        {
            var response = new Response();
            CategoryCreateDTOValidator validator = new CategoryCreateDTOValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    response.Errors.Add(error.ErrorMessage);

                    response.Status = StatusCode.BadRequest;
                    return response;
                }
            }

            var isExist = await _categoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                response.Errors.Add("This category already created");
                response.Status = StatusCode.BadRequest;
                return response;
            }

            var category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };

            await _categoryRepository.CreateAsync(category);
            return response;

        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = new Response();

            var category = await _categoryRepository.GetAsync(id);
            if (category == null)
            {
                response.Errors.Add("Category couldnt found");
                response.Status = StatusCode.NotFound;
                return response;
            }
            await _categoryRepository.DeleteAsync(category);

            return response;

        }

        public async Task<Response<CategoryResponseDTO>> FilterByName(string? name)
        {
            var response = new Response<CategoryResponseDTO>
            {
                Data = new CategoryResponseDTO
                {
                    Categories = await _categoryRepository.FilterByName(name)
                }
            };
            return response;
        }

        public async Task<Response<CategoryResponseDTO>> GetAllAsync(string? title)
        {
            var response = new Response<CategoryResponseDTO>
            {
                Data = new CategoryResponseDTO
                {
                    Categories = await _categoryRepository.FilterByName(title)
                }
            };
            return response;
        }

        public async Task<Response<CategoryItemResponseDTO>> GetAsync(int id)
        {
            var response = new Response<CategoryItemResponseDTO>();
            var category = await _categoryRepository.GetAsync(id);

            if (category == null)
            {
                response.Errors.Add("Category couldnt found");
                response.Status = StatusCode.NotFound;

            }
            response.Data = new CategoryItemResponseDTO
            {
                Category = category
            };
            return response;
        }

        public async Task<Response> UpdateAsync(CategoryUpdateDTO model)
        {
            var response = new Response();
            CategoryUpdateDTOValidator validator = new CategoryUpdateDTOValidator();

            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    response.Errors.Add(error.ErrorMessage);
                    response.Status = StatusCode.BadRequest;
                    return response;
                }
            }
            var isExist = await _categoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != c.Id);

            if (isExist)
            {
                response.Errors.Add("This category already created");
                response.Status = StatusCode.BadRequest;
                return response;
            }

            var category = await _categoryRepository.GetAsync(model.Id);
            if (category == null)
            {
                response.Errors.Add("Category couldnt found");
                response.Status = StatusCode.NotFound;
                return response;

            }
            category.Title = model.Title;
            category.ModifiedAt = DateTime.Now;
            await _categoryRepository.UpdateAsync(category);
            return response;
        }
    }
}
