using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class ConfirmUI : UIPopup, IBaseUI
    {
        [Header("[ Header ]")]
        [SerializeField] 
        private TMP_Text TitleText;
        
        [Header("[ Content ]")]
        [SerializeField] 
        private TMP_Text MessageText;
        
        [Header("[ Bottom ]")]
        [SerializeField] 
        private Button PrimaryBtn;
        [SerializeField]
        private TMP_Text PrimaryText;
        private Action primaryAction;
        
        [SerializeField] 
        private Button CancelBtn;
        [SerializeField]
        private TMP_Text CancelText;
        private Action cancelAction;
        

        public void SetTitle(string text)
        {
            TitleText.text = text;
        }

        public void SetMessage(string text)
        {
            MessageText.text = text;
        }
        
        public void SetPrimary(string text, Action action = null)
        {
            PrimaryText.text = text;

            if (action != null)
            {
                primaryAction = action;   
            }
        }
        
        public void SetCancel(string text, Action action = null)
        {
            CancelText.text = text;

            if (action != null)
            {
                cancelAction = action;   
            }
        }
        
        
        private void Start()
        {
            PrimaryBtn.onClick.AddListener(OnClickPrimary);
            CancelBtn.onClick.AddListener(OnClickCancel);
        }

        private void OnDestroy()
        {
            PrimaryBtn.onClick.RemoveAllListeners();
            CancelBtn.onClick.RemoveAllListeners();
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
        
        
        public void OnInit()
        {
            
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
            
        }
    }
}