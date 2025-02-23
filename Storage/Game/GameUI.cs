using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PhantomEngine
{
    public sealed class GameUI : MonoBehaviour
    {
        [SerializeField] private Transform uiTarget;
        [SerializeField] private GameData uiData;
        
        private readonly List<GameObject> uiContainer = new();
        
        
        private void Start()
        {
            PreLoad().Forget();
        }

        private void OnDestroy()
        {
            Release();
        }


        public async UniTask PreLoad()
        {
            if (uiTarget == null)
                return;

            if (uiData == null)
                return;

            if (uiData.Prefabs == null || uiData.Prefabs.Count == 0)
                return;

            foreach (var uiPrefab in uiData.Prefabs)
            {
                var uiHandle = Addressables.InstantiateAsync(uiPrefab, uiTarget);
                await uiHandle;
                
                var ui = uiHandle.Result;
                uiContainer.Add(ui);
                UIManager.Instance.AddUI(ui);
            }
        }
        
        public void Release()
        {
            if (uiContainer.Count == 0) 
                return;
            
            var uiList = uiContainer.ToList();
            foreach (var ui in uiList)
            {
                UIManager.Instance.RemoveUI(ui);
                uiContainer.Remove(ui);
                Addressables.ReleaseInstance(ui);
            }
        }
    }
}