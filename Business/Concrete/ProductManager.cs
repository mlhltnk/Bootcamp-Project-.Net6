﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
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
        IProductDal _productDal;                
        ICategoryService _categoryService;      

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;      
        }

        
        //[SecuredOperation("admin")]              
        [ValidationAspect(typeof(ProductValidator))]      
        
        [CacheRemoveAspect("IProductService.Get")]   

        [TransactionScopeAspect]            
        public IResult Add(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName), 
                CheckIfProductCountOfCategoryCorrect(product.CategoryID), CheckIfCategoryLimitExceded());                       

            if(result != null)                                                                    
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
               

        }


        [CacheAspect]   //CACHE 
        [PerformanceAspect(5)]  
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }



        [CacheAspect]   //CACHE 
        public IDataResult<List<Product>> GetAll()
        {
            if(DateTime.Now.Hour == 21)  
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
            
        }



        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryID==id));
        }

        

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));     
        }


        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]         
        public IResult Update(Product product)
        {
            //bir kategoride en fazla 10 ürün olabilir
            var result = _productDal.GetAll(p => p.CategoryID == product.CategoryID);          
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }










       
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryid)   
        {
           
            var result = _productDal.GetAll(p => p.CategoryID == categoryid);           
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }



        
        private IResult CheckIfProductNameExist(string productName)   
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();          
            if (result==true)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }


        
        private IResult CheckIfCategoryLimitExceded()             
                                                                   
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

       


       
    }
   
}
