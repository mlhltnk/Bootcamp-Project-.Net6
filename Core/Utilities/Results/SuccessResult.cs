using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message) : base(true, message)   // hem true hem mesaj yollama işlemi
        {

        }

        public SuccessResult():base(true)   //mesajsız true yollama işlemi
        {

        }
    }
}
