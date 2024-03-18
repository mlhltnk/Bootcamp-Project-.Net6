using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new NorthwindContext())
            {
                var result = from operationClaim in context.OperationClaims    //OperationClaims ile userOperationClaim'i joinliyor 
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id      //idsi benim gönderdiğim user'ın idsine eşit olanları buluyor
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };   //opreationclaim olarakda bunları return ediyoruz
                return result.ToList();

            }
        }
    }
}
