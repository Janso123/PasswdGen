using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswdGen

{
    class Program
    {
        private static string tempPassFile = "./passwd.temp";
        private static string passFile = "./passwd.passwd";
        private static string keyToFile;
        private static bool _isPassToShow = false;
        static public List<char> asciiCharAray = new List<char>();
        static public List<char> passwLetters = new List<char>();
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
            asciiAray();
            passwdLetterConv(Dialog.passwdInput());

            string ret = "Passwd: " + passEncryption() + " Palce: " + Dialog.placeInput() + "\r\n";
            if (File.Exists(passFile))
            {
                DecryptFile(passFile, tempPassFile, keyToFile);
            }
            File.AppendAllText(tempPassFile, ret);
            EncryptFile(tempPassFile, passFile, keyToFile);
            File.Delete(tempPassFile);
        }
        static public void ReadPasswdFile()
        {
            TextDialog Dialog = new TextDialog();
            keyToFile = Dialog.keyInput();
            _isPassToShow = true;
            DecryptFile(passFile, tempPassFile, keyToFile);
        }
        static public void asciiAray()
        {
            asciiCharAray = new List<char>();
            char[] c = "!@#$%^&*()abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                asciiCharAray.Add(c[i]);

            }
        }
        static public void passwdLetterConv(string passwd)
        {
            passwLetters = new List<char>();
            for (int i = 0; i < passwd.Length; i++)
            {
                passwLetters.Add(passwd[i]);
            }
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
        private static void EncryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd");
            }
        }
        private static void DecryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    List<byte> output = new List<byte>();
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        if (_isPassToShow)
                                        {
                                            output.Add((byte)data);

                                        }
                                        else
                                            fsOut.WriteByte((byte)data);
                                    }
                                    if (_isPassToShow)
                                    {
                                        byte[] ret = output.ToArray();
                                        Console.WriteLine(Encoding.UTF8.GetString(ret));
                                    }
                                }
                            }
                        }
                    }
                    if (File.Exists(tempPassFile) && _isPassToShow)
                    {
                        File.Delete(tempPassFile);
                        _isPassToShow = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}