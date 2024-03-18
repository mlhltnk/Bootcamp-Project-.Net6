using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entitities.Concrete;
using Entitities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal

    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //contexteki; ürünleri ve kategorileri join et. ürüne p,categoriye c dedik. pdeki CategoryId ile c deki kategori eşitse onları join et.
                //hangi kolonları istiyorsun? sonucu ProductDetailDto kolonlarına uydurarak ver
                //ProductId'yi p'deki ProductId'den al, Productname'i p'deki ProductName'den al ve diğerleri..)
                //return Iquearable olarak döngü olarak döndüğü için return.result.Tolist(); şeklinde yazılır.

                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryID equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductID,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };

                return result.ToList();
            }
        }
    }
}
