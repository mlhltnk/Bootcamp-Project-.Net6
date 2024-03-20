using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependecyResolvers.Autofac;
using Core.DependecyResolvers;
using Core.Extensions;
using Core.Utilities.IOC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();



//---JWT ÝÇÝN YAPILAN TANIMLAMALAR
var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddDependecyResolvers(new ICoreModule[]             //COREMODULE; istediðimiz kadar ekleyebilmek için yazdýk. CoreModule gibi istediðimiz kadar modul oluþturup buraya ekleyebiliriz.
{
    new CoreModule()
});

//-------JWT ÝÇÝN YAPILAN TANIMLAMALAR SON------





builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//.netcore öncesinde program.cs; IOC Container altyapýsý saðlamak için kullanýlýyordu. Autofac bize AOP imkaný sunuyor. Bu sebeple alttaki kodlarý Autofac'e taþýdýk

//builder.Services.AddSingleton<IProductService, ProductManager>();          //Bana arkaplanda bir referans oluþtur.(IOC Container)  //Birisi constructorda Iproductservice isterse ona arka planda productmanager oluþtur ve onu ver.
//builder.Services.AddSingleton<IProductDal, EfProductDal>();                //Birisi IproductDal isterse ona Efporductdal'ý ver



//----.net core altyapýsýnda halihazýrda varolan IOC container kullandýrmak yerine AUTOFAC kullandýrma yapýlandýrmasý-----
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder2 => builder2.RegisterModule(new AutofacBusinessModule()));
//-----------------



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();            //JWT ÝÇÝN TANIMLANDI

app.UseAuthorization();



app.Run();



