using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class TitleUI : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text VersionText;
        [SerializeField] 
        private Button AdminBtn;
        [SerializeField] 
        private Button NewBtn;
        [SerializeField] 
        private Button SettingBtn;
    
        
        private void Awake()
        {
            VersionText.text = $"Version: {Application.version}";
        }
        
        private void Start()
        {
            AdminBtn.onClick.AddListener(OnClickAdmin);
            NewBtn.onClick.AddListener(OnClickNew);
            SettingBtn.onClick.AddListener(OnClickSetting);
        }

        private void OnDestroy()
        {
            AdminBtn.onClick.RemoveAllListeners();
            NewBtn.onClick.RemoveAllListeners();
            SettingBtn.onClick.RemoveAllListeners();
        }


        private void OnClickAdmin()
        {
            if (UserManager.Instance.UserData == null)
            {
                UIManager.Instance.OpenUI<LoginUI>();
            }
            else
            {
                var ui = UIManager.Instance.OpenUI<ConfirmUI>();
                ui.SetTitle("Logout");
                ui.SetMessage("Are you sure you want to switch accounts?");
                ui.SetPrimary("OK", OnClickPrimary);
                ui.SetCancel("Cancel");
            }
        }

        private void OnClickPrimary()
        {
            UserManager.Instance.RemoveUser();
        }
        
        private void OnClickNew()
        {
            
        }

        private void OnClickSetting()
        {
            
        }
    }
}