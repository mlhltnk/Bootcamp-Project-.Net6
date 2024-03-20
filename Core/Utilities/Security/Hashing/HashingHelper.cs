using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper    //Hash oluşturmaya ve onu doğrulamaya yarar
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)   //biz password vereceğiz bize paswwordün hash ve saltını oluşturacak.
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())             //SHA512 algoritmasını kullanarak password hash ve salt oluşturacak
            {
                passwordSalt = hmac.Key;                                                //salt değeri olarak buradaki algoritmanın Key değerini kullanıyoruz.Her kullanıcı için farklı değer oluşturur.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));    //password değerinin byte karşılığını verdik.(strign değerin byte karşılığı bu şekilde veriliyor)
            }
        }


        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)   //PasswordHashini doğrulama yapan kısım(sisteme tekrardan girilirken bi<im dbdeki hash ve salt ile eşleşip eşleşmediğini verilen yer)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }                     
        }
    }

