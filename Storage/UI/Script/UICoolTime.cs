using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    public class UICoolTime : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private float elapsed;
        [SerializeField] private float duration;
        
        private CancellationTokenSource cts;

        
        private void OnDestroy()
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
        }


        [ContextMenu("Play")]
        public void Play()
        {
            cts = new CancellationTokenSource();
            TimerAsync().Forget();
        }

        private async UniTask TimerAsync()
        {
            while (elapsed <= duration)
            {
                elapsed += Time.deltaTime;
                Refresh();
                
                await UniTask.Yield(cts.Token);
            }
        }
        
        [ContextMenu("Pause")]
        public void Pause()
        {
            if (cts != null)
            {
                cts.Cancel();  
            } 
        }

        public void Refresh()
        {
            Debug.Log("Refresh");
            image.fillAmount = (duration - elapsed) / duration;
        }
        
        [ContextMenu("Clear")]
        public void Clear()
        {
            elapsed = 0;
            
            if (cts != null)
            {
                cts.Cancel();  
            } 
        }
    }
}