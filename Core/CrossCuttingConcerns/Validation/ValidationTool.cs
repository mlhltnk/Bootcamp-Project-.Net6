using Core.Utilities.Results;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator,object entity)
        {
            //Bir tane Ivalidator verdik yani PRODUCTVALİDATOR, Ivvalidator fluentvalidation classıdır. yani kurallarımızın olduğu class
            //Bir tane de entity yani PRODUCT verdik. Yani doğrulacak class

            
            var context = new ValidationContext<object>(entity);          //Ivalidatörün Validate metodunu kullanarak kontrol ediyoruz. 
            var result = validator.Validate(context);
            if (!result.IsValid)                                          //Valid değilse altta errors fırlatıyoruz.,sonuç geçerli değilse hata fırlat
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
