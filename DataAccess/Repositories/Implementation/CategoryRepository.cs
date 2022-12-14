using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> FilterByName(string? name)
        {
            return _context.Categories.Where(c => !string.IsNullOrEmpty(name) ? c.Title.Contains(name) : true).ToList();
        }
    }
}
