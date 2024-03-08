using Business.Abstract;
using Business.CCS;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entitities.Concrete;
using Entitities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;       //burada (entityframework,ınmemory vs) bağımlılığı yaratmamak için IproductDal yazılır
 

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
          
        }




        [ValidationAspect(typeof(ProductValidator))]           //VALİDASYONU AOP(ASPECT-AUTOFAC) sayesinde attribute olarak buraya yazdık. metot içine yazmadık
        public IResult Add(Product product)
        {//buraya iş kodları ve validasyon kodları yazılır. ürünü eklemeden önce kurallar varsa buraya yazarız


                _productDal.Add(product);

                return new SuccessResult(Messages.ProductAdded);
                //return new Result(true,"ürün eklendi");   //işlemin sonucu true ve ekrana ürün eklendi yazdırmak için;
                //(true,ürün eklendi) bu kısmın construtor'ı Result.cs de oluşturuldu        
         
        }




        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }


        public IDataResult<List<Product>> GetAll()
        {
            if(DateTime.Now.Hour == 21)  //sistem saati 21 ise sistem bakımda dönecektir.
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
            //DataResult döndürüyorum. çalıştığım tip: List<product>
            //_productDal.GetAll(); döndürdüğüm data.  true;işlem sonucum , mesaj; bilgilendirme mesajı
        }



        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryID==id));
        }

        

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));       //belirtilen 2 fiyat aralığında olan datayı getirecektir.
        }


        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }


        //geri kalan CRUD operasyonlarını yaz.
    }
}
