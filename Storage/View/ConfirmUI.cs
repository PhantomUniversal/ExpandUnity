using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class ConfirmUI : UIPopup, IBaseUI
    {
        [Header("[ Text ]")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_Text primaryText;
        [SerializeField] private TMP_Text cancelText;
        
        [Header("[ Button ]")]
        [SerializeField] private Button primaryBtn;
        private Action primaryAction;
        [SerializeField] private Button cancelBtn;
        private Action cancelAction;
        

        public void SetTitle(string text)
        {
            titleText.text = text;
        }

        public void SetMessage(string text)
        {
            messageText.text = text;
        }
        
        public void SetPrimary(string text, Action action = null)
        {
            primaryText.text = text;
            primaryAction = action;
        }
        
        public void SetCancel(string text, Action action = null)
        {
            cancelText.text = text;
            cancelAction = action;
        }
        
        
        private void Start()
        {
            primaryBtn.onClick.AddListener(OnClickPrimary);
            cancelBtn.onClick.AddListener(OnClickCancel);
        }

        private void OnDestroy()
        {
            primaryBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.RemoveAllListeners();
        }

        
        private void OnClickPrimary()
        {
            primaryAction?.Invoke();
            OnClose();
        }

        private void OnClickCancel()
        {
            cancelAction?.Invoke();
            OnClose();
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
            
        }
    }
}