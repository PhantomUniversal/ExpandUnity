using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] 
        private Button TouchBtn;
        [SerializeField] 
        private TMP_Text StatusText;


        private void Awake()
        {
            Progress();
        }

        private void OnEnable()
        {
            UserManager.Instance.OnUserChanged += OnUserChanged;
        }

        private void Start()
        {
            TouchBtn.onClick.AddListener(OnClickTouch);
        }

        private void OnDestroy()
        {
            TouchBtn.onClick.RemoveAllListeners();
        }

        private void OnDisable()
        {
            UserManager.Instance.OnUserChanged -= OnUserChanged;
        }


        private void OnUserChanged()
        {
            StatusText.text = UserManager.Instance.UserData == null ? "로그인 하기" : "게임 시작";
        }
        
        private void OnClickTouch()
        {
            Progress();
        }

        
        private void Progress()
        {
            if (!UserManager.Instance.RefreshUser())
            {
                UIManager.Instance.OpenUI<LoginUI>();
                return;
            }
            
            
        }
    }
}