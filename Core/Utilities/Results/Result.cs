using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
      
        public Result(bool success, string message): this(success)   //this(success); Result'ın(this demek classın kendisi demektir) tek parametreli olan construtoruna successi yolla
                                                                     //bu sayede hem success hem message istenirse bu ve alttaki construtor aynı anda çalışır.  
                                                                     //bu constructor sayesinde programcının başarı dönüşümlerini(örnek: return new Result(true,"ürün eklendi");) 
                                                                     //standart hale getirdik.Bizim dizaynımız dışına çıkamasın diye.                                                              
        {
            Message = message;                                        //Aşağıdaki message değerini Message'a ata.      
        }


        public Result(bool success)                             //Mesaj boşsa bu constructor çalışır ikiside doluysa üstteki çalışır buna OVERLOADİNG deniyor.                                            
        {
                                                
            Success = success;

        }


        public bool Success { get; }

        public string Message { get; }                      //normalde bu bir getterdır, set edilemez. Ancak sadece construtor ile getterlar set edilebilir.Bu sebeple en üst satırda construtor oluşturduk
    }
}
