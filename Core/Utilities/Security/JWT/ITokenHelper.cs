using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //Kullanıcı, kullanıcıadı ve parolasını yazıp butona bastığında CreateToken metodu çalışacak.
        //Eğer doğruysa ilgili user için Dbye gidecek DB'de bu kullancının Claimlerini bulacak. İçerisinde bu bilgileri barındıran JWT üretecek ve bu bilgileri kullanıcıya verecek.
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
