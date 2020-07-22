using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHashingAndSalt
{
    class Program
    {
        static List<PasswortDaten> passwortDatens = new List<PasswortDaten>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write("Passwort: ");
                string eingabe = Console.ReadLine();

                PasswortDaten neuerPasswortEintrag = new PasswortDaten();
                neuerPasswortEintrag.Passwort = eingabe;

                byte[] saltz = GeneriereSalt();

                neuerPasswortEintrag.Salt = Convert.ToBase64String(saltz);

                neuerPasswortEintrag.Hash = Convert.ToBase64String(GeneriereHash(neuerPasswortEintrag.Passwort, saltz));

                passwortDatens.Add(neuerPasswortEintrag);
            }







            foreach (var item in passwortDatens)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        static byte[] GeneriereSalt()
        {
            byte[] salt = new byte[16];

            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }

        static byte[] GeneriereHash(string passwort, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(passwort, salt, 10000);

            return pbkdf2.GetBytes(20);
        }
    }

    class PasswortDaten
    {
        public string Passwort { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public override string ToString()
        {
            return $"{Passwort} + {Salt}: {Hash}";
        }
    }
}
