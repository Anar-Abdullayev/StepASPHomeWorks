using ECommerce.Core.DataAccess;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Abstractions
{
    public interface IProductDal:IEntityRepository<Product>
    {
        //custom methods
    }
}
