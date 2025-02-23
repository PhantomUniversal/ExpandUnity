namespace PhantomEngine
{
    public class UIRegex
    {
        public const string NAME = @"^[가-힣a-zA-Z\s]{2,20}$";
        
        public const string EMAIL = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        
        public const string PASSWORD = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$";
    }
}