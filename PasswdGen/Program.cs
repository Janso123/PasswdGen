using System;
using System.Collections.Generic;
using System.IO;

namespace PasswdGen

{
    class Program
    {
        private static string tempPassString;
        private static string passFile = "./passwd.passwd";
        private static string keyToFile;
        private static List<char> asciiCharAray = new List<char>("!@#$%^&*()abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray());
        private static List<char> passwLetters = new List<char>();
        private static Encryption Encrypt = new Encryption();
        private static Decryption Decrypt = new Decryption();

        static void Main(string[] args)
        {
            while (Menu()) { };
        }

        static public bool Menu()
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
        static public void NewPasswdDialog()
        {
            TextDialog Dialog = new TextDialog();

            keyToFile = Dialog.keyInput();
            passwLetters = new List<char>(Dialog.passwdInput().ToCharArray());
            string ret = "Passwd: " + passEncryption() + " Palce: " + Dialog.placeInput() + "\r\n";
            if (File.Exists(passFile))
            {
                tempPassString = Decrypt.File(passFile, keyToFile);
            }
            tempPassString = tempPassString + ret;
            
            Encrypt.File(tempPassString, passFile, keyToFile);
        }
        static public void ReadPasswdFile()
        {
            TextDialog Dialog = new TextDialog();
            Decryption EnDe = new Decryption();
            keyToFile = Dialog.keyInput();
            Console.WriteLine(Decrypt.File(passFile, keyToFile));
        }

        static public string passEncryption()
        {
            string encPasswd = "";
            Random rnd = new Random();
            for (int i = 0; i < passwLetters.Count; i++)
            {
                int randchar = rnd.Next(1, 555) % 6;
                for (int a = 0; a < asciiCharAray.Count; a++)
                {
                    if (asciiCharAray[a] == (passwLetters[i]))
                    {
                        char hpa = (Char)(Convert.ToUInt16(asciiCharAray[a]) + randchar);
                        encPasswd = encPasswd + hpa.ToString();
                    }
                }
            }

            return encPasswd;
        }
        
    }
}