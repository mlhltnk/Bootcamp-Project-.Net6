using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        //false+data+mesaj
        {

        }

        public ErrorDataResult(T data) : base(data, false)
        //false+data
        {

        }

        public ErrorDataResult(string message) : base(default, false, message)
        //default dataya karşılık geliyor
        //mesaj+false+default data
        {

        }

        public ErrorDataResult() : base(default, false)
        //false+default data
        {

        }
    }
}
