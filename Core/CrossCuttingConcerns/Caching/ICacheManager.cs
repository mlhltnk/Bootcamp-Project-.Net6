using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager   //bütün cache metdolarını kapsar(redis,inmemory vs.)
    {
        T Get<T>(string key);                               //ben sana bir key vereyim sen bu keye denk gelen datayı bana dbden ver

        object Get(string key);
        void Add(string key, object value, int duration);   //value=cachede herşey yutabiliriz bu sebeple value,  duration:cache tutma süresi

        bool IsAdd(string key);   //cachede var mı?

        void Remove(string key);    //cacheden silme

        void RemoveByPattern(string pattern);
    }
}
