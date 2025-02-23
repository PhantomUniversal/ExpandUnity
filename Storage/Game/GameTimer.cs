using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PhantomEngine
{
    public class GameTimer : MonoBehaviour
    {
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