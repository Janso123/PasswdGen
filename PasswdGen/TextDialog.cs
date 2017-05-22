
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

            Console.WriteLine("Enter some passwd to generate new passwd");
            oldPasswd = Console.ReadLine();
            return oldPasswd;
        }
        public string placeInput()
        {
            Console.Clear();

            Console.WriteLine("Enter where you want to use that passwd");
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
                this.keyInput();
            }
            Console.Clear();
            return passwdToEncryptDecrypt;
        }
    }
}
