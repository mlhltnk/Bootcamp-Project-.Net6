using Core.Utilities.Results;
using Entitities.Concrete;
using Entitities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();                    //IDataResult;Hem işlem sanucu hem mesajı hemde döndüreceği şeyi(List<product>) içerir    //IResult;işlem sanucu ve hem mesajı içerir

        IDataResult<List<Product>> GetAllByCategoryId(int id);                      //kategoriId'ye göre productları getir

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);

        IDataResult<List<ProductDetailDto>> GetProductDetails();

        IResult Add(Product product);                                              //ürün ekleme  (bu void olduğu için bunu Iresult olarak bıraktık)

        IResult Update(Product product);

        IDataResult<Product> GetById(int productId);                               //Id'ye göre tek bir ürün getir

        //geri kalan CRUD operasyonlarını yaz!!!
    }
}
