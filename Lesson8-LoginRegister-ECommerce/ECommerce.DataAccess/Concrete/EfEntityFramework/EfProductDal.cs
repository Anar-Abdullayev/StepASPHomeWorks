using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstractions;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Concrete.EfEntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public EfProductDal(NorthwindContext context) : base(context)
        {
        }
    }
}
