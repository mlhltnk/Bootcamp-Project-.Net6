using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business;

public class BusinessRules   //BURASI İŞ MOTORU
{
    public static IResult Run(params IResult[] logics)  //siz params verdiğiniz zaman run içine istediğin kadar IResult TÜRÜNDE istediğimiz kadar PARAMETRE verebiliyosun (logics=iş kuralı demek)
    {                                                   //params kullandığımız zaman params tipinde istediğimiz kadar parametreyi managerda geçebiliriz.

        foreach (var logic in logics)       //bütün logic(iş kuralılarını) gez
        {
            if (!logic.Success)             //kurala uymayan varsa
            {
                return logic;                //error result döndürecek
                                             //parametre ile gönderdiğimiz iş kurallarından başarısız olanlarını(logic hatalarını) businessa bildiriyoruz
            }      
        }
        return null;                         //başarılı ise hiçbişe göndermesine gerek yok
    }
}
