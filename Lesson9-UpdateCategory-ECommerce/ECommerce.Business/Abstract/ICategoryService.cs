using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetAsync(Expression<Func<Category, bool>> filter);
        Task<string> UpdateAsync(int categoryId, string categoryName);
        Task<string> AddCategoryAsync(Category category);
    }
}
