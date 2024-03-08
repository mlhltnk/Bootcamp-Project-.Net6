using Entitities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    //kategori ile ilgili dış dünyaya neyi servis etmek istiyorsak o operasyonları yazıyoruz.
    {
        List<Category> GetAll();

        Category GetById(int categoryId);
    }
}
