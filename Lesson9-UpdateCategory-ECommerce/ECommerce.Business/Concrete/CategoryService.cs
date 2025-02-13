using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstractions;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<string> AddCategoryAsync(Category category)
        {
            var result = await _categoryDal.Get(c => c.CategoryName == category.CategoryName);
            if (result is not null)
                return "Category exists";
            await _categoryDal.Add(category);
            return "Succeed";
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryDal.GetList();
        }

        public async Task<Category> GetAsync(Expression<Func<Category, bool>> filter)
        {
            return await _categoryDal.Get(filter);
        }

        public async Task<string> UpdateAsync(int categoryId, string categoryName)
        {
            try
            {
                Category result;
                result = await _categoryDal.Get(c => c.CategoryName == categoryName);
                if (result is not null)
                    return "Category exists";
                result = await _categoryDal.Get(c => c.CategoryId == categoryId);
                if (result is null)
                    return "Not found";
                result.CategoryName = categoryName;
                await _categoryDal.Update(result);
                return "Succeed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
