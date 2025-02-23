using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class ForgotUI : UIPanel, IBaseUI
    {
        [Header("[ Header ]")] 
        [SerializeField] private Button CloseBtn;
        [SerializeField] private Button RefreshBtn;

        [Header("[ Content ]")] 
        [SerializeField] private TMP_InputField EmailInput;
        [SerializeField] private Button PrimaryBtn;
        
        
        private void Start()
        {
            RefreshBtn.onClick.AddListener(OnClickRefresh);
            CloseBtn.onClick.AddListener(OnClickClose);
            PrimaryBtn.onClick.AddListener(OnClickPrimary);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
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
            UIManager.Instance.RefreshUI<ForgotUI>();
        }
        
        private void OnClickClose()
        {
            UIManager.Instance.CloseUI<ForgotUI>();
        }

        
        private void OnClickPrimary()
        {
            if (string.IsNullOrEmpty(EmailInput.text))
            {
                AlertPopup("[이메일 입력 필수]", "이메일 입력이 필요합니다.");
                return;
            }
            
            if (!Regex.IsMatch(EmailInput.text, UIRegex.EMAIL))
            {
                AlertPopup("[이메일 규칙]", "올바른 이메일 형태로 입력이 필요합니다.");
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
            EmailInput.Select();
#endif
            EmailInput.text = string.Empty;
        }
    }
}