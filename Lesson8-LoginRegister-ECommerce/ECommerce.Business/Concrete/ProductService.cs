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
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productDal.GetList();
        }

        public async Task<List<Product>> GetAllByCategoryAsync(int categoryId)
        {
            return await _productDal.GetList(p => p.CategoryId == categoryId || categoryId == 0);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productDal.Get(p=>p.ProductId == id);
        }
    }
}
