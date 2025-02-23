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
        [SerializeField] private Button closeBtn;
        [SerializeField] private Button refreshBtn;
        
        [Header("[ Content ]")]
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField passwordInput;
        [SerializeField] private Button registerBtn;
        [SerializeField] private Button forgotBtn;
        [SerializeField] private Button primaryBtn;

        
        private void Start()
        {
            closeBtn.onClick.AddListener(OnClickClose);
            refreshBtn.onClick.AddListener(OnClickRefresh);
            registerBtn.onClick.AddListener(OnClickRegister);
            forgotBtn.onClick.AddListener(OnClickForgot);
            primaryBtn.onClick.AddListener(OnClickPrimary);    
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (emailInput.isFocused)
                {
                    passwordInput.Select();
                }

                if (passwordInput.isFocused)
                {
                    emailInput.Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                primaryBtn.onClick.Invoke();
            }
        }

        private void OnDestroy()
        {
            closeBtn.onClick.RemoveListener(OnClickClose);
            refreshBtn.onClick.RemoveListener(OnClickRefresh);
            registerBtn.onClick.RemoveListener(OnClickRegister);
            forgotBtn.onClick.RemoveListener(OnClickForgot);
            primaryBtn.onClick.RemoveListener(OnClickPrimary);
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
            if (string.IsNullOrEmpty(emailInput.text))
            {
                AlertPopup("알림", "이메일 입력이 필요합니다.");
                return;
            }
            
            if (!Regex.IsMatch(emailInput.text, UIRegex.EMAIL))
            {
                AlertPopup("알림", "이메일 입력이 필요합니다.");
                return;
            }
            
            if (string.IsNullOrEmpty(passwordInput.text))
            {
                AlertPopup("알림", "패스워드 입력이 필요합니다.");
                return;
            }
            
            if (!Regex.IsMatch(passwordInput.text, UIRegex.PASSWORD))
            {
                AlertPopup("알림", "비밀번호는 8자리 이상, 영문/숫자/특수문자를 포함합니다.");
                return;
            }
            
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
            SetPlay(UIHash.OPEN);
        }

        public void OnClose()
        {
            SetPlay(UIHash.CLOSE);
        }

        public void OnRefresh()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            emailInput.Select();
#endif
            emailInput.text = string.Empty;
            passwordInput.text = string.Empty;
        }
    }
}