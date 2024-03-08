using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //BU CLASS TÜM YAZILIMLARDA STANDART !!

    public interface IEntityRepository<T> where T : class, IEntity, new()    //generic constraint işlemi ; T'yi kısıtmalama işlemi (sadece '"class" referans tip' olabilir ve
                                                                            //'"IEntity" Ientitity implemente eden bir nesne olabilir' ve
                                                                            //new()  : newlenebilir olmalı 
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);         //hem hepsini getirebilmemizi, hemde expression sayesinde filtre ile getirebilmemizi sağlar
                                                                      //filter=null ; filtre vermeyedebilirsin demektir

        T Get(Expression<Func<T, bool>> filter);                      //tek getirme senaryosunda bu kullanılır
         
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}
