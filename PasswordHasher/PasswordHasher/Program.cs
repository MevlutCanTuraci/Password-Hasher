using System.Data.SqlTypes;

namespace PasswordHasher
{
    public class Program
	{
        static PasswordHasherHelper _helper = new PasswordHasherHelper();

        public static void Main()
        {
            #region Password hash generator function #1

            //GenerateHash();

            #endregion


            #region Password hash generator function #2

            GenerateHashShort();

            #endregion


            #region Password verify function

            //! Password is 1234
            //VerifyPasswordHash();

            #endregion

            Console.Read();
        }


        private static void GenerateHash()
        {
            string myPassword = "1234";
            string saltString = string.Empty, hashString = string.Empty;

            byte[] salt = _helper.GenerateSalt(out saltString);
            byte[] hash = _helper.GenerateHash(salt, myPassword, out hashString);

            Console.WriteLine($"Your password: \t{myPassword}\r\nYour salt: \t{saltString}\r\nYour hash: \t{hashString}");
        }


        private static void GenerateHashShort()
        {
            string myPassword = "1234";
            (string Salt, string Hash) hasherResult = _helper.CreateHash(myPassword);
            
            Console.WriteLine($"Your password: \t{myPassword}\r\nYour salt: \t{hasherResult.Salt}\r\nYour hash: \t{hasherResult.Hash}");
        }


        private static void VerifyPasswordHash()
        {
            //string myPassword = "1234";
            string saltString = "lUZbZisyHShnCKm/E3HIkw==", hashString = "f60HgyRN9TIgGOB7OfPT6Ke4Vxz36Ccsa/p8+OJuLuw=";

            Console.Write("Enter password: ");
            string userPassword = Console.ReadLine();

            #region Using #1

            byte[] salt = Convert.FromBase64String(saltString);
            byte[] hash = Convert.FromBase64String(hashString);

            bool isVerifyPassword1 = _helper.VerifyPassword(salt, hash, userPassword);

            #endregion


            #region Using #2 

            bool isVerifyPassword2 = _helper.VerifyPassword(saltString, hashString, userPassword);

            #endregion

            if (isVerifyPassword1)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("(1)-Password is Correct.");
            }
            if (isVerifyPassword2)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("(2)-Password is Correct.");
            }
            else
            {
                Console.WriteLine("Invalid Password!");
            }
        }

    }
}