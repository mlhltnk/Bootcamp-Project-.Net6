using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper  //JWT sistemini yönetebilmen için anahtarın-->securitykey; şifreleme algoritman-->HmacSha512Signature şeklindedir metodu
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)              //JWT'ların oluşturulabilmesi için gerekli Credentails kayıtlarıdır.
        {
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
