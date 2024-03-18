using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data,bool success,string message):base(success,message)   //veri,true ve mesaj dönderir
        {
            Data = data;
        }

        public DataResult(T data, bool success):base(success)   //veri ve true döner
        {
            Data = data;
        }
       

        public T Data { get; }
    }
}
