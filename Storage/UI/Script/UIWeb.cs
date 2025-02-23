using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    [RequireComponent(typeof(Button))]
    public class UIWeb : MonoBehaviour
    {
        [SerializeField] private string webUrl;
        
        
        public void SetUrl(string url)
        {
            webUrl = url;
        }


        private void Start()
        {
            if (!TryGetComponent(out Button component))
                return;
            
            component.onClick.AddListener(OpenWeb);
        }

        private void OnDestroy()
        {
            if (!TryGetComponent(out Button component))
                return;
            
            component.onClick.RemoveListener(OpenWeb);
        }


        private void OpenWeb()
        {
            Application.OpenURL(webUrl);
        }
    }
}