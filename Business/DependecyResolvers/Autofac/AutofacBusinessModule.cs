using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependecyResolvers.Autofac;

public class AutofacBusinessModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {   //eskiden bunu program.cs'ye yazardık. Autofac; uygulamaya yayına alındığında bellekte referansları oluşturuyor(reflaksion)

        builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();      //Birisi constructorda Iproductservice isterse ona arka planda productmanager oluştur ve onu ver.
        builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();            //Birisi constructorda IproductDal isterse ona arka planda EfProductDal oluştur ve onu ver.

        builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();


        //---JWT İŞLEMİNDEN SONRA EKLENEN DEPENDECYLER
        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        //-------JWT SON---------
       

        //-------------alttaki kısım sayesinde  Autofac aracılığı ile interceptor özelliği de eklemiş oluyoruz.
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}
