using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace D.Net.Sec
{
    public static class EncryptDecryptUtil
    {
       // public static string SystemDefaultKey = GenerateNewKey();

        //public static string EncryptString(string inputText)
        //{
        //    return EncryptString(inputText, SystemDefaultKey);
        //}
        //public static string EncryptString(string inputText, string password)
        //{
            
        //    var aes = System.Security.Cryptography.Aes.Create();
            
        //    RijndaelManaged rijndaelCipher = new RijndaelManaged();
        //    // First we need to turn the input strings into a byte array.
        //    byte[] plainText = System.Text.Encoding.Unicode.GetBytes(inputText);
        //    // Using salt to make it harder to guess our key
        //    // using a dictionary attack.
        //    byte[] salt = Encoding.ASCII.GetBytes(password.Length.ToString());
        //    // The (Secret Key) will be generated from the specified
        //    // password and salt.
        //    //PasswordDeriveBytes -- It Derives a key from a password
        //    PasswordDeriveBytes secretKey = new PasswordDeriveBytes(password, salt);
        //    // Create a encryptor from the existing SecretKey bytes.
        //    // We use 32 bytes for the secret key
        //    // (the default Rijndael key length is 256 bit = 32 bytes) and
        //    // then 16 bytes for the IV (initialization vector),
        //    // (the default Rijndael IV length is 128 bit = 16 bytes)
        //    ICryptoTransform Encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(16), secretKey.GetBytes(16));
        //    //rijndaelCipher.GenerateKey();
        //    //string key = Convert.ToBase64String(rijndaelCipher.Key);
        //    // Create a MemoryStream that is going to hold the encrypted bytes
        //    MemoryStream memoryStream = new MemoryStream();
        //    // Create a CryptoStream through which we are going to be processing our data.
        //    // CryptoStreamMode.Write means that we are going to be writing data
        //    // to the stream and the output will be written in the MemoryStream
        //    // we have provided. (always use write mode for encryption)
        //    CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
        //    // Start the encryption process.
        //    cryptoStream.Write(plainText, 0, plainText.Length);
        //    // Finish encrypting.
        //    cryptoStream.FlushFinalBlock();
        //    // Convert our encrypted data from a memoryStream into a byte array.
        //    byte[] CipherBytes = memoryStream.ToArray();
        //    // Close both streams.
        //    memoryStream.Close();
        //    cryptoStream.Close();
        //    // Convert encrypted data into a base64-encoded string.
        //    // A common mistake would be to use an Encoding class for that.
        //    // It does not work, because not all byte values can be
        //    // represented by characters. We are going to be using Base64 encoding
        //    // That is designed exactly for what we are trying to do.
        //    string encryptedData = Convert.ToBase64String(CipherBytes);
        //    // Return encrypted string.
        //    return encryptedData;
        //}
        //public static string DecryptString(this string inputText)
        //{
        //    return DecryptString(inputText, SystemDefaultKey);
        //}
        //public static string DecryptString(string InputText, string Password)
        //{
        //    try
        //    {
        //        RijndaelManaged RijndaelCipher = new RijndaelManaged();
        //        byte[] EncryptedData = Convert.FromBase64String(InputText);
        //        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
        //        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
        //        // Create a decryptor from the existing SecretKey bytes.
        //        ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));
        //        MemoryStream memoryStream = new MemoryStream(EncryptedData);
        //        // Create a CryptoStream. (always use Read mode for decryption).
        //        CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
        //        // Since at this point we don't know what the size of decrypted data
        //        // will be, allocate the buffer long enough to hold EncryptedData;
        //        // DecryptedData is never longer than EncryptedData.
        //        byte[] PlainText = new byte[EncryptedData.Length];
        //        // Start decrypting.
        //        int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
        //        memoryStream.Close();
        //        cryptoStream.Close();
        //        // Convert decrypted data into a string.
        //        string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
        //        // Return decrypted string.
        //        return DecryptedData;
        //    }
        //    catch (Exception exception)
        //    {
        //        return (exception.Message);
        //    }
        //}
        //public static string GenerateNewKey()
        //{
        //    RijndaelManaged rijndaelCipher = new RijndaelManaged();
        //    rijndaelCipher.GenerateKey();
        //    return Convert.ToBase64String(rijndaelCipher.Key);
        //}
    }
}
