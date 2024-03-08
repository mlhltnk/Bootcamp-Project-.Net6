using Core.DataAccess;
using Entitities.Concrete;
using Entitities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();    //buraya özel metot tanımı yaptık.
    }
}
