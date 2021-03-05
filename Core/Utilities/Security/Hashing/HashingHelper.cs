using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        // Bu sınıf ona verdiğimiz password'un hash'ını ve tuz'unu oluşturacak.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Biz bir password verecegiz ve disariya bu iki degeri(passwordSalt/passwordHash) cikaracak bir yapi tasarlayacagiz.
            // Disposable Pattern ile kodlayalim
            // HMACSHA512 -> Bu algoritmayi kullanarak biz bir password,salt ve hash olustaracagiz.
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                // Encoding.UTF8.GetBytes(password)  -> Bu kod ile string ifadenin byte degerini aldik.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        // out keyword'u kullanilmayacak
        // burada ki passwordHash veri tabani degerimizdir
        // sisteme tekrardan girmeye calisirken olladigi parola
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
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
