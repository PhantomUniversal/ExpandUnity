using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class AgreeUI : UIPopup, IBaseUI
    {
        [Header("[ Agree ]")]
        [SerializeField] 
        private Toggle AllToggle;
        [SerializeField] 
        private Toggle AgeToggle;
        [SerializeField]
        private Toggle TermsOfUseToggle;
        [SerializeField] 
        private Button TermsOfUseBtn;
        [SerializeField]
        private Toggle PrivacyPoliceToggle;
        [SerializeField] 
        private Button PrivacyPolicyBtn;
        [SerializeField] 
        private Button PrimaryBtn;


        public bool ToggleCheck => AgeToggle.isOn && TermsOfUseToggle.isOn && PrivacyPoliceToggle.isOn;


        private void Start()
        {
            AllToggle.onValueChanged.AddListener(OnAllChanged);
            AgeToggle.onValueChanged.AddListener(OnToggleChanged);
            TermsOfUseToggle.onValueChanged.AddListener(OnToggleChanged);
            PrivacyPoliceToggle.onValueChanged.AddListener(OnToggleChanged);
            
            TermsOfUseBtn.onClick.AddListener(OnClickTermsOfUse);
            PrivacyPolicyBtn.onClick.AddListener(OnClickPrivacyPolicy);
            PrimaryBtn.onClick.AddListener(OnClickPrimary);
        }

        private void OnDestroy()
        {
            AllToggle.onValueChanged.RemoveListener(OnAllChanged);
            AgeToggle.onValueChanged.RemoveListener(OnToggleChanged);
            TermsOfUseToggle.onValueChanged.RemoveListener(OnToggleChanged);
            PrivacyPoliceToggle.onValueChanged.RemoveListener(OnToggleChanged);
            
            TermsOfUseBtn.onClick.RemoveListener(OnClickTermsOfUse);
            PrivacyPolicyBtn.onClick.RemoveListener(OnClickPrivacyPolicy);
            PrimaryBtn.onClick.RemoveListener(OnClickPrimary);
        }


        private void OnAllChanged(bool value)
        {
            AgeToggle.isOn = value;
            TermsOfUseToggle.isOn = value;
            PrivacyPoliceToggle.isOn = value;
        }

        private void OnToggleChanged(bool value)
        {
            if (value && ToggleCheck)
            {
                PrimaryBtn.interactable = true;
            }
            else
            {
                PrimaryBtn.interactable = false;
            }
        }
        
        
        private void OnClickTermsOfUse()
        {
            var noticeUI = UIManager.Instance.OpenUI<NoticeUI>();
            noticeUI.SetTitle("이용약관");
            noticeUI.SetNotice("테스트 이용약관 입니다.");
            noticeUI.SetPrimary("확인", () => { TermsOfUseToggle.isOn = true; });
        }

        private void OnClickPrivacyPolicy()
        {
            var noticeUI = UIManager.Instance.OpenUI<NoticeUI>();
            noticeUI.SetTitle("개인정보 처리");
            noticeUI.SetNotice("개인정보 처리 테스트 입니다.");
            noticeUI.SetPrimary("확인", () => { PrivacyPoliceToggle.isOn = true; });
        }

        private void OnClickPrimary()
        {
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
            AllToggle.isOn = false;
            AgeToggle.isOn = false;
            TermsOfUseToggle.isOn = false;
            PrivacyPolicyBtn.interactable = false;
            PrimaryBtn.interactable = false;
        }
    }
}