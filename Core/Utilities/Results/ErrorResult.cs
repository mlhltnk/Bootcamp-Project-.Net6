using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message) : base(false, message)   //base'e(resulta) hem true hem mesaj yollama işlemi
        {

        }

        public ErrorResult() : base(false)   //base'e(resulta) true yollama işlemi
        {

        }
    }
}
