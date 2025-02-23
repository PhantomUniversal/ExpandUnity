using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PhantomEngine
{
    public class UIAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform ui;
        [SerializeField] private float start;
        [SerializeField] private float end;
        [SerializeField] private float result;
        [SerializeField] private float duration;
        [SerializeField] private float timer;

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Leap().Forget();
            }
        }
        
        private async UniTask Leap()
        {
            result = 0f;
            timer = 0f;
            
            while (timer < duration)
            {
                result = Mathf.Lerp(start, end, timer / duration);
                timer += Time.deltaTime;
                Refresh();
                
                await UniTask.Yield();
            }

            result = end;
            Refresh();
        }

        private void Refresh()
        {
            ui.anchoredPosition = new Vector2(result, 0);
        }
    }
}