using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswdGen
{
    class Encryption
    {
        public void File(string inputString, string outputFile, string skey)
        {

            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);
                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (MemoryStream msIn = new MemoryStream(Encoding.UTF8.GetBytes(inputString)))
                                {
                                    int data;
                                    while ((data = msIn.ReadByte()) != -1)
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
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
