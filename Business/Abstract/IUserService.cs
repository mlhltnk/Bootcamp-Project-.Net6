using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);  //claimleri listele
        void Add(User user);   //user ekle
        User GetByMail(string email);   //emaile göre user getir
    }
}
