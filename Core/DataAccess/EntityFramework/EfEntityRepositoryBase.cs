using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Core.DataAccess.EntityFramework;

public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext,new()
{

    public void Add(TEntity entity)
    {
        using (TContext context = new TContext())    //using yazma işlemi performans için yazıldı. yazmayıp direk new'lesende çalışır. Belleği hızlıca temizlemeye yarar
        {

            var addEntity = context.Entry(entity);    //referansı yakalama işlemi
            addEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }



    public void Delete(TEntity entity)
    {
        using (TContext context = new TContext())    //using yazma işlemi performans için yazıldı. yazmayıp direk new'lesende çalışır. Belleği hızlıca temizlemeye yarar
        {
            var deletedEntity = context.Entry(entity);    //referansı yakalama işlemi
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }


    public TEntity Get(Expression<Func<TEntity, bool>> filter)           //tek data getirir
    {
        using (TContext context = new TContext())
        {
            return context.Set<TEntity>().SingleOrDefault(filter);
        }
    }


    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext context = new TContext())
        {
            return filter == null                                       //filtre null mı?
                ? context.Set<TEntity>().ToList()                        //nullsa burası çalışır    (select * from products)
                : context.Set<TEntity>().Where(filter).ToList();        //null değilse burası çalışır
        }
    }


    public void Update(TEntity entity)
    {
        using (TContext context = new TContext())    //using yazma işlemi performans için yazıldı. yazmayıp direk new'lesende çalışır. Belleği hızlıca temizlemeye yarar
        {
            var updatedEntity = context.Entry(entity);    //referansı yakalama işlemi
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
