using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomEngine
{
    public class GameScene : GenericSingleton<GameScene>
    {
        protected override void OnInitialized()
        {
            SceneManager.activeSceneChanged += OnChanceScene;
        }

        protected override void OnDisposed()
        {
            SceneManager.activeSceneChanged -= OnChanceScene;
        }

        
        private void OnChanceScene(Scene previousScene, Scene currentScene)
        {
#if UNITY_EDITOR
            Debug.Log($"Previous scene: {previousScene.name}, Current scene: {currentScene.name}");            
#endif
        }
        
        
        public void LoadScene(string targetScene, string loadingScene = "Loading")
        {
            LoadSceneAsync(targetScene, loadingScene).Forget();
        }

        private async UniTask LoadSceneAsync(string targetScene, string loadingScene)
        {
            var loadingOperation = SceneManager.LoadSceneAsync(loadingScene);
            if (loadingOperation == null)
            {
                return;
            }
            
            while (!loadingOperation.isDone)
            {
                await UniTask.Yield();
            }

            var targetOperation = SceneManager.LoadSceneAsync(targetScene);
            if (targetOperation == null)
            {
                return;
            }
            
            targetOperation.allowSceneActivation = false;

            float targetProgress = 0f;
            while (targetProgress < 0.9f)
            {
                targetProgress = targetOperation.progress;
                await UniTask.Yield();
            }

            await UniTask.Delay(1000);
            targetOperation.allowSceneActivation = true;
        }
    }
}