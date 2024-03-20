using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper   //appsetting.jsonda yazdığımız securitykey(string) idafe ile encryptiona parametre geçemiyoruz
                                     //securitykeyhelper değerini bir byte[] haline getirmemiz gerekiyor. Bu metot bu işlemi yapıyor. JWT'miz bu yapıya ihtiyaç duyuyor
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
