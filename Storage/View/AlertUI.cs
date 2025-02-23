using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class AlertUI : UIPopup, IBaseUI
    {
        [Header("[ Text ]")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_Text primaryText;

        [Header("[ Button ]")]
        [SerializeField] private Button primaryBtn;
        private Action primaryAction;
        
        
        public void SetTitle(string title)
        {
            titleText.text = title;
        }

        public void SetMessage(string message)
        {
            messageText.text = message;
        }
        
        public void SetPrimary(string primary, Action action = null)
        {
            primaryText.text = primary;
            primaryAction = action;
        }

        
        private void Start()
        {
            primaryBtn.onClick.AddListener(OnClickPrimary);
        }

        private void OnDestroy()
        {
            primaryBtn.onClick.RemoveListener(OnClickPrimary);
        }
        
        
        private void OnClickPrimary()
        {
            primaryAction?.Invoke();
            OnClose();
        }
        
        
        public void OnInit()
        {
            
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