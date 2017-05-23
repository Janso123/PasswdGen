using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswdGen
{
    internal class Decryption
    {
        private string _srtOut;
        public string File(string inputFile, string skey)
        {
            try
            {
                using (var aes = new RijndaelManaged())
                {
                    var key = Encoding.UTF8.GetBytes(skey);

                    var iv = Encoding.UTF8.GetBytes(skey);

                    using (var fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (var msOut = new MemoryStream())
                        {
                            using (var decryptor = aes.CreateDecryptor(key, iv))
                            {
                                using (var cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        msOut.WriteByte((byte)data);
                                    }
                                    msOut.Position = 0;
                                    var t = msOut.ToArray();
                                    _srtOut = Encoding.UTF8.GetString(t);
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
            return _srtOut;
        }
    }
}
