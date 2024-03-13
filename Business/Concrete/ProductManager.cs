using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
        IProductDal _productDal;                //burada (entityframework,ınmemory vs) bağımlılığı yaratmamak için IproductDal yazılır
        ICategoryService _categoryService;      //buraya IcategoryDal enjekte edemeyiz.

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;      
        }



        [ValidationAspect(typeof(ProductValidator))]                                             //VALİDASYONU AOP(ASPECT-AUTOFAC) sayesinde attribute olarak buraya yazdık. metot içine yazmadık
        public IResult Add(Product product)
        {//buraya iş kodları ve validasyon kodları yazılır. ürünü eklemeden önce kurallar varsa buraya yazarız

            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName), 
                CheckIfProductCountOfCategoryCorrect(product.CategoryID), CheckIfCategoryLimitExceded());                        //bunlar iş kuralları; params sayesinde aynı satırda yazabildik

            if(result != null)                                                                    //kurallara uymayan bir durum oluşmuşsa
            {
                return result;
            }
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


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            //bir kategoride en fazla 10 ürün olabilir
            var result = _productDal.GetAll(p => p.CategoryID == product.CategoryID);           //kategorideki ürünleri verir
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }


        //BİRDEN ÇOK YERDE KULLANILAN İŞ KURALLARI İÇİN ALLTAKİ ŞEKİLDE AYRI METOTLAR OLUŞTURULUP ÜSTTE ÇAĞIRILIR.

        //bir kategoride en fazla 10 ürün olabilir iş kuralı
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryid)   //kategorideki ürün sayısının kurallara uygunluğunu doğrula
        {
            //select count(*) from products where categoryıd=1 'in LINQ hali aşağıdaki
            var result = _productDal.GetAll(p => p.CategoryID == categoryid);           //kategorideki ürünleri verir
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }



        //Aynı isimde ürün eklenemesin iş kuralı
        private IResult CheckIfProductNameExist(string productName)   
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();  //Any-->var mı? demektir. Yani bu şarta uyan data var mı demektir.        
            if (result==true)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }


        //Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemesin
        private IResult CheckIfCategoryLimitExceded()              //bu kural tek başına bir servis ise categorymanagerda'da yazılabilirdi.
                                                                   //burada productservice categoryserviceden sadece Data verisi aldığı için buraya yazdık. categorymanagera'da yazılabilir.
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }


        //geri kalan CRUD operasyonlarını yaz.
    }
   
}
