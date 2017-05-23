using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PasswdGen

{
    internal class Program
    {
        private static string _tempPassString;
        public static readonly string PassFile = "./passwd.passwd";
        private static string _keyToFile;
        private static readonly List<char> AsciiCharAray = new List<char>("!@#$%^&*()abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray());
        private static List<char> _passwLetters = new List<char>();
        private static readonly Encryption Encrypt = new Encryption();
        private static readonly Decryption Decrypt = new Decryption();
        private static readonly TextDialog Dialog = new TextDialog();

        private static void Main()
        {
            while (Menu()) { }
        }

        public static bool Menu()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1 - Add new Passwd");
            Console.WriteLine("2 - Read Passwd");
            Console.WriteLine("ESC - Exit");


            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Escape:
                    Console.Clear();
                    return false;

                case ConsoleKey.D1:
                    Console.Clear();
                    NewPasswdDialog();
                    Console.Clear();
                    return true;

                case ConsoleKey.D2:
                    Console.Clear();
                    ReadPasswdFile();
                    Console.WriteLine("Click ENTER to continue");
                    Console.ReadLine();
                    Console.Clear();
                    return true;

            }
            return true;
        }
        public static void NewPasswdDialog()
        {
            _keyToFile = Dialog.KeyInput();
            _passwLetters = new List<char>(Dialog.PasswdInput().ToCharArray());
            var ret = string.Format("Passwd: {0} Palce: {1}\r\n", PassEncryption(), Dialog.PlaceInput());
            if (File.Exists(PassFile))
            {
                _tempPassString = Decrypt.File(PassFile, _keyToFile);
            }
            _tempPassString = string.Format("{0}{1}", _tempPassString, ret);
            
            Encrypt.File(_tempPassString, PassFile, _keyToFile);
        }
        public static void ReadPasswdFile()
        {
            _keyToFile = Dialog.KeyInput();
            Console.WriteLine(Decrypt.File(PassFile, _keyToFile));
        }

        public static string PassEncryption()
        {
            var rnd = new Random();
            var randchar = rnd.Next(1, 555) % 6;

            return _passwLetters.Aggregate("", (current1, t) 
                => (from t1 in AsciiCharAray where t1 == t select (Char) (Convert.ToUInt16(t1) + randchar)).Aggregate(current1, (current, hpa) 
                    => string.Format("{0}{1}", current, hpa)));
        }
        
    }
}