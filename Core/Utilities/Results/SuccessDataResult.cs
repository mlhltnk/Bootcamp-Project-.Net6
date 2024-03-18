using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results;

public class SuccessDataResult<T>:DataResult<T>
{
    public SuccessDataResult(T data,string message):base(data,true,message)
    //mesaj+true+data
    {

    }

    public SuccessDataResult(T data):base(data,true)
        //true+data
    {
        
    }

    public SuccessDataResult(string message):base(default,true,message)
        //default dataya karşılık geliyor
        //mesaj+true+default data
    {
        
    }

    public SuccessDataResult():base(default,true)
    //true+default data
    {

    }
}
