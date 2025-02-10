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
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {
        public EfCategoryDal(NorthwindContext context) : base(context)
        {
        }
    }
}
