using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswdGen
{
    class Decryption
    {
        private string srtOut;
        public string File(string inputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (MemoryStream msOut = new MemoryStream())
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        msOut.WriteByte((byte)data);
                                    }
                                    msOut.Position = 0;
                                    byte[] t = msOut.ToArray();
                                    srtOut = Encoding.UTF8.GetString(t);
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
            return srtOut;
        }
    }
}
