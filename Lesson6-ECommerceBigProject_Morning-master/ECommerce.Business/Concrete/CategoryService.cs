using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstractions;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CategoryService(ICategoryDal categoryDal) : ICategoryService
    {
        public async Task<List<Category>> GetAllAsync()
        {
            return await categoryDal.GetList();
        }
    }
}
