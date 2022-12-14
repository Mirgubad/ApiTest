using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<Response<CategoryResponseDTO>> GetAllAsync(string? title);
        Task<Response<CategoryItemResponseDTO>> GetAsync(int id);
        Task<Response<CategoryResponseDTO>> FilterByName(string? name);
        Task<Response> CreateAsync(CategoryCreateDTO model);
        Task<Response> UpdateAsync(CategoryUpdateDTO model);
        Task<Response> DeleteAsync(int id);
    }
}
