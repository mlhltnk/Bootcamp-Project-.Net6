using Core.Utilities.IOC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions  //CORE SEVİYESİNDE EKLEYECEĞİMİZ BÜTÜN İNJECTİONLARI TEK NOKTADA TOPLADIK
                                                    //core seviyesinde injectionları program.csye eklemek yerine burayı oluşturduk.Buraya ekleyeceğiz.
    {
        public static IServiceCollection AddDependecyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)  //this yanına yazdığın Neyi genişletmek istediğin neyse odur.
        {
            foreach (var module in modules)   //eklenen herbir module için module'ü yükle
            {
                module.Load(serviceCollection); 
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
