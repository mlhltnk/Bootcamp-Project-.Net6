using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken     //erişim anahtarı
    {
        //kullanıcı adı ve parolasını verene bir TOKEN ve ne zaman sonlanacağı verisini vereceğiz
        public string Token { get; set; }            //JWT değerimiz bu olacak
        public DateTime Expiration { get; set; }     //Tokenın bitiş zamanı
    }
}
