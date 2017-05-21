
using System;

namespace PasswdGen
{

    class TextDialog
    {
        private string oldPasswd;
        private string placeToLogIn;
        private string passwdToEncryptDecrypt;

        public string passwdInput()
        {
            Console.Clear();

            Console.WriteLine("Enter your passwd to generate new passwd");
            oldPasswd = Console.ReadLine();
            return oldPasswd;
        }
        public string placeInput()
        {
            Console.Clear();

            Console.WriteLine("Enter where you will use that passwd");
            placeToLogIn = Console.ReadLine();
            return placeToLogIn;
        }
        public string keyInput()
        {
            Console.Clear();

            Console.WriteLine("Enter your Key to encrypt / decrypt your passwd storage file \r\n ");
            Console.WriteLine("Key have to be 16 characters long ");
            passwdToEncryptDecrypt = Console.ReadLine();
            if (passwdToEncryptDecrypt.Length !=16)
            {
                Console.WriteLine("You enterd wrong key \r\n key have to be 16 characters long");
            }
            Console.Clear();
            return passwdToEncryptDecrypt;
        }
    }
}
