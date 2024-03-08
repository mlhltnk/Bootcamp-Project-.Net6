using Business.Abstract;
using DataAccess.Abstract;
using Entitities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
       //Dış dünyaya servis etmek istediğim iş kodlarımı buraya yazıyorum
    {
        ICategoryDal _categoryDal;

        //categorymanager olarak veri erişim katmanına(categorydala) bağlıyım ama zayıf bağlıyım.
        //Dependecyinjection sayesinde bunu sağlıyoruz
        public CategoryManager(ICategoryDal categoryDal)  
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetById(int categoryId)
        {
            return _categoryDal.Get(c => c.CategoryId == categoryId);
        }
    }
}
