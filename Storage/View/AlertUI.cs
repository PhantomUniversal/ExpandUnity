using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class AlertUI : UIPopup, IBaseUI
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
        
        
        public void SetTitle(string title)
        {
            TitleText.text = title;
        }

        public void SetMessage(string message)
        {
            MessageText.text = message;
        }
        
        public void SetPrimary(string primary, Action action = null)
        {
            PrimaryText.text = primary;

            if (action != null)
            {
                primaryAction = action;   
            }
        }

        
        private void Start()
        {
            PrimaryBtn.onClick.AddListener(OnClickPrimary);
        }

        private void OnDestroy()
        {
            PrimaryBtn.onClick.RemoveAllListeners();
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