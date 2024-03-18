using Core.Utilities.IOC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependecyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //biri senden IHttpContextAccessor isterse ona HttpContextAccessor ver
            //HttpContextAccessor; her yapılan istekle ilgili başlangıçtan bitişe kadar kullanıcının isteğinin takip edilmesini yapar
            //TÜM PROJELERİNDE BU İNJECTİONU YAPACAĞIZ BU SEBEPLE BURAYA YAZDIK
        }
    }
}
