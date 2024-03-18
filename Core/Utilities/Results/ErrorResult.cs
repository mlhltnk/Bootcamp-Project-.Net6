using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message) : base(false, message)   //hem false hem mesaj yollama işlemi
        {

        }

        public ErrorResult() : base(false)   //mesajsız false yollama işlemi
        {

        }
    }
}
