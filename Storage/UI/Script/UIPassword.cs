using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    [AddComponentMenu("UI/UIPassword")]
    [RequireComponent(typeof(Button))]
    public class UIPassword : MonoBehaviour
    {
        [SerializeField] private TMP_InputField passwordInput;
        [SerializeField] private Image passwordIcon;
        [SerializeField] private Sprite passwordShow;
        [SerializeField] private Sprite passwordHide;
        
        
        private bool IsVisible { get; set; }
        
        
        private void Start()
        {
            if (!TryGetComponent(out Button component))
                return;
            
            component.onClick.AddListener(OnClickAction);    
        }

        private void OnDestroy()
        {
            if (!TryGetComponent(out Button component))
                return;
            
            component.onClick.RemoveListener(OnClickAction);   
        }


        private void OnClickAction()
        {
            IsVisible = !IsVisible;
            passwordIcon.sprite = IsVisible ? passwordHide : passwordShow;
            passwordInput.contentType = IsVisible ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        }
    }
}