using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class CategoryUI : UIPopup, IBaseUI
    {
        [Header("[ Text ]")]
        [SerializeField] private TMP_Text primaryText;
        [SerializeField] private TMP_Text secondaryText;
        [SerializeField] private TMP_Text cancelText;
        
        [Header("[ Button ]")]
        [SerializeField] private Button primaryBtn;
        private Action primaryAction;
        [SerializeField] private Button secondaryBtn;
        private Action secondaryAction;
        [SerializeField] private Button cancelBtn;
        private Action cancelAction;
        
        
        public void SetPrimary(string text, Action action = null)
        {
            primaryText.text = text;
            primaryAction = action;
        }

        public void SetSecondary(string text, Action action = null)
        {
            secondaryText.text = text;
            secondaryAction = action;
        }
        
        public void SetCancel(string text, Action action = null)
        {
            cancelText.text = text;
            cancelAction = action;
        }
        
        
        private void Start()
        {
            primaryBtn.onClick.AddListener(OnClickPrimary);
            secondaryBtn.onClick.AddListener(OnClickSecondary);
            cancelBtn.onClick.AddListener(OnClickCancel);
        }

        private void OnDestroy()
        {
            primaryBtn.onClick.RemoveAllListeners();
            secondaryBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.RemoveAllListeners();
        }
        
        
        private void OnClickPrimary()
        {
            primaryAction?.Invoke();
            OnClose();
        }
        
        private void OnClickSecondary()
        {
            secondaryAction?.Invoke();
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