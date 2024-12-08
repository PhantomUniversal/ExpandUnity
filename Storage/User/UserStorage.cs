using UnityEngine;

namespace PhantomEngine
{
    [CreateAssetMenu(fileName = "UserStorage", menuName = "Phantom/UserStorage")]
    public class UserStorage : ScriptableObject
    {
        [SerializeField, TextArea(10, 20)] 
        public string TermsOfUse;
        [SerializeField, TextArea(10, 20)] 
        public string PrivacyPolicy;
    }
}