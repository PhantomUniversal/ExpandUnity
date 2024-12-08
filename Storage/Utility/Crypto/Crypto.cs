using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace PhantomEngine
{
    public class Crypto
    {
        public static void Save(string cryptoHash, object cryptoJson)
        {
            var cryptoKey = CryptoHelper.GenerateLoadKey(32);
            var cryptoValue = JsonConvert.SerializeObject(cryptoJson);
            var cryptoData = Encrypt(cryptoKey, cryptoValue);

            if (string.IsNullOrEmpty(cryptoData))
                return;
            
            PlayerPrefs.SetString(cryptoHash, cryptoData);
            PlayerPrefs.Save();
        }

        public static void Remove(string cryptoHash)
        {
            PlayerPrefs.DeleteKey(cryptoHash);
        }
        
        public static T Load<T>(string cryptoHash)
        {
            var cryptoKey = CryptoHelper.GenerateLoadKey(32);
            if (!PlayerPrefs.HasKey(cryptoHash))
            {
                return default;
            }
            
            var cryptoData = PlayerPrefs.GetString(cryptoHash);
            var cryptoValue = Decrypt(cryptoKey, cryptoData);
            var cryptoJson = JsonConvert.DeserializeObject<T>(cryptoValue);
            return cryptoJson;
        }
        
        public static string Encrypt(byte[] key, string value)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = CryptoHelper.GenerateRandomKey(16);
            aesAlg.KeySize = 256;
            aesAlg.BlockSize = 128;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
                
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] encryptedEncoding = Encoding.UTF8.GetBytes(value);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(encryptedEncoding, 0, encryptedEncoding.Length);
                
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(byte[] key, string value)
        {
            byte[] buffer = Convert.FromBase64String(value);
            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = CryptoHelper.GenerateRandomKey(16);
            aesAlg.KeySize = 256;
            aesAlg.BlockSize = 128;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}