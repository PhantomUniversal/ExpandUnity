using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class NoticeUI : UIPopup, IBaseUI
    {
        [Header("[ Header ]")]
        [SerializeField] 
        private TMP_Text TitleText;
        
        [Header("[ Content ]")]
        [SerializeField]
        private ScrollRect NoticeScroll;
        [SerializeField] 
        private TMP_Text NoticeText;
        [SerializeField] 
        private Button PrimaryBtn;
        [SerializeField]
        private TMP_Text PrimaryText;
        private Action PrimaryAction { get; set; }
        

        public void SetTitle(string title)
        {
            TitleText.text = title;
        }

        public void SetNotice(string notice)
        {
            NoticeText.text = notice;
            NoticeScroll.content.sizeDelta = new Vector2(NoticeScroll.content.sizeDelta.x, notice.Length);
            NoticeScroll.verticalNormalizedPosition = 0;
        }

        public void SetPrimary(string primary, Action action = null)
        {
            PrimaryText.text = primary;

            if (action != null)
            {
                PrimaryAction = action;   
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
            PrimaryAction?.Invoke();
            OnClose();
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