using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class RegisterUI : UIPanel, IBaseUI
    {
        [Header("[ Header ]")] 
        [SerializeField] private Button CloseBtn;
        [SerializeField] private Button RefreshBtn;

        [Header("[ Content ]")] 
        [SerializeField] private TMP_InputField EmailInput;
        [SerializeField] private TMP_InputField PasswordInput;
        [SerializeField] private Button PrimaryBtn;
        
                                                                                                                                                                           
        private void Start()
        {
            RefreshBtn.onClick.AddListener(OnClickRefresh);
            CloseBtn.onClick.AddListener(OnClickClose);
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
            RefreshBtn.onClick.RemoveAllListeners();
            CloseBtn.onClick.RemoveAllListeners();
            PrimaryBtn.onClick.RemoveAllListeners();
        }

        
        private void OnClickRefresh()
        {
            UIManager.Instance.RefreshUI<RegisterUI>();
        }
        
        private void OnClickClose()
        {
            UIManager.Instance.CloseUI<RegisterUI>();
        }

        
        private void OnClickPrimary()
        {
            if (string.IsNullOrEmpty(EmailInput.text))
            {
                AlertPopup("알림", "이메일 입력이 필요합니다.");
                return;
            }
            
            if (!Regex.IsMatch(EmailInput.text, UIRegex.EMAIL))
            {
                AlertPopup("알림", "유효한 이메일 주소를 입력해주세요.");
                return;
            }
            
            if (string.IsNullOrEmpty(PasswordInput.text))
            {
                AlertPopup("알림", "패스워드 입력이 필요합니다.");
                return;
            }
            
            if (!Regex.IsMatch(PasswordInput.text, UIRegex.PASSWORD))
            {
                AlertPopup("알림", "비밀번호는 8자 이상, 영문/숫자/특수문자를 포함해야 합니다.");
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
            
            UIManager.Instance.OpenUI<AgreeUI>();
        }

        public void OnClose()
        {
            SetPlay(UIHash.CLOSE);
        }

        public void OnRefresh()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            EmailInput.Select();
#endif
            EmailInput.text = string.Empty;
            PasswordInput.text = string.Empty;
            
            UIManager.Instance.OpenUI<AgreeUI>();
        }
    }
}