using DataAccess.Abstract;
using Entitities.Concrete;
using Entitities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;                        //veri tabanım bu, bir liste olarak simule ettim

        public InMemoryProductDal()                     //ramde çalıştırdığımız için ürünleri simüle edip, oluşturdum
        {
            _products = new List<Product>()
            {
                new Product{ProductID=1, CategoryID=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=10},
                new Product{ProductID=2, CategoryID=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                new Product{ProductID=3, CategoryID=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product{ProductID=4, CategoryID=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                new Product{ProductID=5, CategoryID=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1}

            };
        }



        public void Add(Product product)
        {
           _products.Add(product);
        }



        public void Delete(Product product)
        {
             Product producttodelete = _products.SingleOrDefault(p=>p.ProductID==product.ProductID);   //her p için p.productid'si benim gönderdiğim productid'ye eşit olanı getir
                                                                                                     //Firstordefult ve singleordefault aynıdır(amaç tek eleman getirmesini sağlamak)
            _products.Remove(producttodelete);
        }





        public List<Product> GetAll()
        {
            return _products;
        }


        public void Update(Product product)
        {
            //göndermdiğim üsrün idsine sahip olan, ürün listesindeki ürünü bul
            Product producttoupdate = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            producttoupdate.ProductName = product.ProductName;
            producttoupdate.ProductID = product.ProductID;
            producttoupdate.UnitPrice = product.UnitPrice;
            producttoupdate.UnitsInStock = product.UnitsInStock;
        }


        public List<Product> GetAllByCategory(int categoryId)
        {
           return _products.Where(p => p.CategoryID == categoryId).ToList();   //where; içindeki koşula uyanlarla ilgili bir liste yapar
                                                                               //categoriid'ye göre listeleme yapar
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
