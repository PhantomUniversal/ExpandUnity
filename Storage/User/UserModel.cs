using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomEngine
{
    [Serializable]
    public class UserAccount
    {
        private readonly List<UserTable> userContainer = new();

        
        public void AddUser(UserTable table)
        {
            if (userContainer.Contains(table))
                return;
            
            userContainer.Add(table);
        }

        public void RemoveUser(UserTable table)
        {
            if (userContainer.Count == 0)
                return;
            
            if (!userContainer.Contains(table))
                return;
            
            userContainer.Remove(table);
        }

        public bool FindUser(UserTable table)
        {
            return userContainer.Count != 0 && userContainer.Contains(table);
        }
    }

    [Serializable]
    public class UserTable
    {
        public string ID { get; private set; }
        public string Password { get; private set; }
        
        
        public UserTable(string id, string password)
        {
            ID = id;
            Password = password;
        }
    }
    
    [Serializable]
    public class UserData
    {
        public string ID { get; private set; }
        public UserToken Token { get; private set; }
        public PlatformType Platform { get; private set; }
        public string Uid { get; private set; }
        public DateTime Date { get; private set; }

        
        public UserData(string userID, string accessToken, string refreshToken, PlatformType accountPlatform)
        {
            ID = userID;
            Token = new UserToken(accessToken, refreshToken);
            Platform = accountPlatform;
            Uid = SystemInfo.deviceUniqueIdentifier;
            Date = DateTime.UtcNow;
        }
    }

    [Serializable]
    public class UserToken
    {
        public string Access { get; private set; }
        public string Refresh { get; private set; }


        public UserToken(string accessToken, string refreshToken)
        {
            Access = accessToken;
            Refresh = refreshToken;
        }
    }

    [Serializable]
    public class UserRequest
    {
        public string ID { get; set; }
        public string Token { get; set; }
        public string Uid { get; set; }
    }
}