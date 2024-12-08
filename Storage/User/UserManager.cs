namespace PhantomEngine
{
    public class UserManager : GenericSingleton<UserManager>
    {
        public UserData UserData { get; private set; }

        public delegate void OnUserChangedDelegate();
        public OnUserChangedDelegate OnUserChanged;
        
        protected override void OnInitialized()
        {
            
        }

        protected override void OnDisposed()
        {
            
        }

        
        public void SetUser(string userID, string accessToken, string refreshToken, PlatformType accountPlatform)
        {
            UserData = new UserData(userID, accessToken, refreshToken, accountPlatform);
            OnUserChanged?.Invoke();
        }
        
        public void SaveUser()
        {
            if (UserData == null)
                return;
            
            Crypto.Save(UserHash.KEY, UserData);
        }

        public void RemoveUser()
        {
            Crypto.Remove(UserHash.KEY);
        }
        
        public UserData LoadUser()
        {
            return Crypto.Load<UserData>(UserHash.KEY);
        }
        
        public bool RefreshUser()
        {
            UserData = LoadUser();
            OnUserChanged?.Invoke();
            return UserData != null;
        }
    }
}