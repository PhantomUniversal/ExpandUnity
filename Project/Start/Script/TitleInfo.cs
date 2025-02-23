using TMPro;
using UnityEngine;

namespace PhantomEngine
{
    public class TitleInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text versionText;
        
        
        private void Awake()
        {
            versionText.text = $"Version: {Application.version}";
        }
    }
}