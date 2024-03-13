using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependecyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Autofac,Ninject,Castlewindsor,strutureMap,Dryinject -->.netcore öncesinde burasý IOC Container altyapýsý saðlamak için kullanýlýyordu. Autofac bize AOP imkaný sunuyor. Bu sebeple burayý Autofac'e taþýdýk

//builder.Services.AddSingleton<IProductService, ProductManager>();          //Bana arkaplanda bir referans oluþtur.(IOC Container)  //Birisi constructorda Iproductservice isterse ona arka planda productmanager oluþtur ve onu ver.
//builder.Services.AddSingleton<IProductDal, EfProductDal>();               //Birisi IproductDal isterse ona Efporductdal'ý ver



//----.net core altyapýsýnda default gelen IOC container yerine AUTOFAC kullanma yapýlandýrmasý-----
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder2 => builder2.RegisterModule(new AutofacBusinessModule()));
//-----------------



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();



