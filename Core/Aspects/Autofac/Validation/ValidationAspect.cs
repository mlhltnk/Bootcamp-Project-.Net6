using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    //ASPECT: METODUN BAŞINDA SONUNDA HATA VERDİĞİNDE ÇALIŞACAK YAPI DEMEKTİR.!
    //INVOCATION: METOdumuz (şuan için add metoduna yaptık) 
    public class ValidationAspect : MethodInterception   //Aspect
    {
        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            //defensive code
            if (!typeof(IValidator).IsAssignableFrom(validatorType))                 //gönderdiğin validatör type bir Ivalidator mu onu sorguluyor değilse hata düşürüyüor
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }



        protected override void OnBefore(IInvocation invocation)                        //sen sadece Onbefore'u ez.yani (metoddan önce çalış)methodInterception içindeki sadece Onbefore çalışacak.
        {                                                                               //başında olma sebebi buradaki işimiz validation olduğu için doğrulama metodun haşında yapışlır bu sebeple onbeforu'u ezdik
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];          //productın tipini yakalama
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);                 //validationTool.cs'yi kullanarak validate ettik.
            }
        }
    }
}
