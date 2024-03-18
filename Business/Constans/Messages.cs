using Core.Entities.Concrete;
using Entitities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans;

public static class Messages
{
    public static string ProductAdded = "Ürün Eklendi";
    public static string ProductNameInvalid = "Ürün ismi Geçersiz";
    public static string MaintenanceTime = "sistem bakımda";
    public static string ProductsListed ="ürünler listelendi";
    public static string ProductCountOfCategoryError = "Bir ategoride en fazla 10 ürün olabilir";
    public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
    public static string CategoryLimitExceded = "Kategori limiti aşıldığı için ürün eklenemiyor";
    public static string AuthorizationDenied = "yetkiniz yok";
    public static string UserRegistered;
    public static User UserNotFound;
    public static User PasswordError;
    public static string SuccessfulLogin = "Başarrılı giriş";
    public static string UserAlreadyExists = "kullanıcı mevcut";
    public static string AccessTokenCreated = "Token oluşturuldu";
}
