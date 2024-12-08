using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class LoginUI : UIPanel, IBaseUI
    {
        [Header("[ Header ]")]
        [SerializeField] private Button CloseBtn;
        [SerializeField] private Button RefreshBtn;
        
        [Header("[ Content ]")]
        [SerializeField] private TMP_InputField EmailInput;
        [SerializeField] private TMP_InputField PasswordInput;
        [SerializeField] private Button RegisterBtn;
        [SerializeField] private Button ForgotBtn;
        [SerializeField] private Button PrimaryBtn;

        
        private void Start()
        {
            CloseBtn.onClick.AddListener(OnClickClose);
            RefreshBtn.onClick.AddListener(OnClickRefresh);
            RegisterBtn.onClick.AddListener(OnClickRegister);
            ForgotBtn.onClick.AddListener(OnClickForgot);
            PrimaryBtn.onClick.AddListener(OnClickPrimary);    
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (EmailInput.isFocused)
                {
                    PasswordInput.Select();
                }

                if (PasswordInput.isFocused)
                {
                    EmailInput.Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                PrimaryBtn.onClick.Invoke();
            }
        }

        private void OnDestroy()
        {
            CloseBtn.onClick.RemoveListener(OnClickClose);
            RefreshBtn.onClick.RemoveListener(OnClickRefresh);
            RegisterBtn.onClick.RemoveListener(OnClickRegister);
            ForgotBtn.onClick.RemoveListener(OnClickForgot);
            PrimaryBtn.onClick.RemoveListener(OnClickPrimary);
        }

        
        private void OnClickClose()
        {
            UIManager.Instance.CloseUI<LoginUI>();
        }

        private void OnClickRefresh()
        {
            UIManager.Instance.RefreshUI<LoginUI>();
        }
        
        private void OnClickRegister()
        {
            UIManager.Instance.OpenUI<RegisterUI>();
        }

        private void OnClickForgot()
        {
            UIManager.Instance.OpenUI<ForgotUI>();
        }
        
        private void OnClickPrimary()
        {
            if (string.IsNullOrEmpty(EmailInput.text))
            {
                AlertPopup("알림", "이메일 입력이 필요합니다.");
                return;
            }
            
            if (!Regex.IsMatch(EmailInput.text, UserRegex.EMAIL))
            {
                AlertPopup("알림", "이메일 입력이 필요합니다.");
                return;
            }
            
            if (string.IsNullOrEmpty(PasswordInput.text))
            {
                AlertPopup("알림", "패스워드 입력이 필요합니다.");
                return;
            }
            
            if (!Regex.IsMatch(PasswordInput.text, UserRegex.PASSWORD))
            {
                AlertPopup("알림", "비밀번호는 8자리 이상, 영문/숫자/특수문자를 포함합니다.");
                return;
            }

            if (!PlayerPrefs.HasKey(UserHash.EMAIL) || !PlayerPrefs.HasKey(UserHash.PASSWORD))
            {
                AlertPopup("알림", "등록되지 않은 계정입니다. 회원가입을 진행해주세요.");
                return;
            }

            if (PlayerPrefs.GetString(UserHash.EMAIL) != EmailInput.text)
            {
                AlertPopup("알림", "등록되지 않은 계정입니다. 회원가입을 진행해주세요.");
                return;
            }
            
            if (PlayerPrefs.GetString(UserHash.PASSWORD) != PasswordInput.text)
            {
                AlertPopup("알림", "비밀번호가 올바르지 않습니다. 다시 입력해주세요.");
                return;
            }
            
            UserManager.Instance.SetUser(EmailInput.text, "Bearer test1234", "Refresh test1234", PlatformType.Email);
            OnClose();
        }
        
        private void AlertPopup(string title, string message, Action action = null)
        {
            var ui = UIManager.Instance.OpenUI<AlertUI>();
            ui.SetTitle(title);
            ui.SetMessage(message);
            ui.SetPrimary("확인", action);
        }
        
        
        public void OnOpen()
        {
            SetPlay(UIAnimation.OPEN);
        }

        public void OnClose()
        {
            SetPlay(UIAnimation.CLOSE);
        }

        public void OnRefresh()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            EmailInput.Select();
#endif
            EmailInput.text = string.Empty;
            PasswordInput.text = string.Empty;
        }
    }
}