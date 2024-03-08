using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult         //T; hangi tiple çalışacağını özel olarak belirt demektir.
                                                    //Hem işlem sanucu hem mesajı hemde döndüreceği şeyi(List<product>) içeren yapılandırma burasıdır.
    {
        T Data { get; }                             //Data; örn: list<product>
    }
}
