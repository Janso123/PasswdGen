using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswdGen
{
    internal class Encryption
    {
        public void File(string inputString, string outputFile, string skey)
        {

            try
            {
                using (var aes = new RijndaelManaged())
                {
                    var key = Encoding.UTF8.GetBytes(skey);
                    var iv = Encoding.UTF8.GetBytes(skey);
                    using (var fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (var encryptor = aes.CreateEncryptor(key, iv))
                        {
                            using (var cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (var msIn = new MemoryStream(Encoding.UTF8.GetBytes(inputString)))
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
