
using System;

namespace PasswdGen
{
    internal class TextDialog
    {
        private string _oldPasswd;
        private string _placeToLogIn;
        private string _passwdToEncryptDecrypt;

        public string PasswdInput()
        {
            Console.Clear();

            Console.WriteLine("Enter some passwd to generate new passwd");
            _oldPasswd = Console.ReadLine();
            return _oldPasswd;
        }
        public string PlaceInput()
        {
            Console.Clear();

            Console.WriteLine("Enter where you want to use that passwd");
            _placeToLogIn = Console.ReadLine();
            return _placeToLogIn;
        }
        public string KeyInput()
        {
            Console.Clear();

            Console.WriteLine("Enter your Key to encrypt / decrypt your passwd storage file \r\n ");
            Console.WriteLine("Key have to be 16 characters long ");
            _passwdToEncryptDecrypt = Console.ReadLine();
            if (_passwdToEncryptDecrypt.Length !=16)
            {
                this.KeyInput();
            }
            Console.Clear();
            return _passwdToEncryptDecrypt;
        }
    }
}
