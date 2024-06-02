using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken     //erişim anahtarı
    {
        
        public string Token { get; set; }            //JWT değerimiz bu olacak
        public DateTime Expiration { get; set; }     //Tokenın bitiş zamanı
    }
}
