using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class UIPassword : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField PasswordInput;
        [SerializeField] 
        private Button PasswordBtn;
        [SerializeField] 
        private Image PasswordImage;
        [SerializeField] 
        private Sprite PasswordShow;
        [SerializeField] 
        private Sprite PasswordHide;

        
        private bool PasswordVisible { get; set; }
        
        
        private void Start()
        {
            PasswordBtn.onClick.AddListener(OnClickAction);    
        }

        private void OnDestroy()
        {
            PasswordBtn.onClick.RemoveAllListeners();
        }


        private void OnClickAction()
        {
            PasswordVisible = !PasswordVisible;
            PasswordImage.sprite = PasswordVisible ? PasswordHide : PasswordShow;
            PasswordInput.contentType = PasswordVisible ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        }
    }
}