using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class TitleUI : MonoBehaviour
    {
        [SerializeField] private Button playBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private Button adminBtn;
        
        
        private void Start()
        {
            playBtn.onClick.AddListener(OnClickPlay);
            exitBtn.onClick.AddListener(OnClickExit);
            adminBtn.onClick.AddListener(OnClickAdmin);
        }

        private void OnDestroy()
        {
            playBtn.onClick.RemoveListener(OnClickPlay);
            exitBtn.onClick.RemoveListener(OnClickExit);
            adminBtn.onClick.RemoveListener(OnClickAdmin);
        }

        
        private void OnClickPlay()
        {
            GameScene.Instance.LoadScene("Lobby");
        }
        
        private void OnClickExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;            
#else
            Application.Quit();
#endif
        }

        private void OnClickAdmin()
        {
            var ui = UIManager.Instance.OpenUI<CategoryUI>();
            ui.SetPrimary("Github", OnClickGithub);
            ui.SetSecondary("Notion", OnClickNotion);
            ui.SetCancel("Cancel");
        }

        private void OnClickGithub()
        {
            Application.OpenURL("https://github.com/PhantomUniversal");
        }

        private void OnClickNotion()
        {
            Application.OpenURL("https://phantomuniversal.notion.site/308bb82d49354350947d3ba6144f3883?pvs=4");
        }
    }
}