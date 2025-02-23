using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] 
        private Button ProgressBtn;
        [SerializeField] 
        private TMP_Text ProgressText;
        [SerializeField] 
        private TitlePatch ProgressPatch;
        
        
        private void Start()
        {
            ProgressBtn.onClick.AddListener(OnClickProgress);
        }

        private void OnDestroy()
        {
            ProgressBtn.onClick.RemoveListener(OnClickProgress);
        }
        
        
        private void OnClickProgress()
        {
            ProgressAsync().Forget();
        }


        private async UniTask ProgressAsync()
        {
            await ProgressPatch.UpdateContentAsync();
            
            GameScene.Instance.LoadScene("Lobby");
        }
    }
}