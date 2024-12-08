using System;
using System.Security.Cryptography;
using UnityEngine;

namespace PhantomEngine
{
    public class CryptoHelper
    {
        public static byte[] GenerateLoadKey(int length)
        {
            byte[] key;
            if (PlayerPrefs.HasKey(CryptoHash.Key))
            {
                var keyBase = PlayerPrefs.GetString(CryptoHash.Key);
                key = Convert.FromBase64String(keyBase);
            }
            else
            {
                key = GenerateRandomKey(length);
                var keyBase = Convert.ToBase64String(key);
                PlayerPrefs.SetString(CryptoHash.Key, keyBase);
                PlayerPrefs.Save();
            }

            return key;
        }
        
        public static byte[] GenerateRandomKey(int length)
        {
            var randomBytes = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}