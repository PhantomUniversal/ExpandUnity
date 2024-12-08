using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace PhantomEngine
{
    public sealed class UIManager : GenericSingleton<UIManager>
    {
        private readonly Dictionary<Type, UIInfo> uiContainer = new();
        private readonly string uiLayer = "UI";
        
        protected override void OnInitialized()
        {
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.pixelPerfect = false;
            canvas.sortingOrder = 10;
            canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
            canvas.vertexColorAlwaysGammaSpace = true;
            
            CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920f, 1080f);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
            canvasScaler.referencePixelsPerUnit = 100f;
            
            GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = false;
            graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
            graphicRaycaster.blockingMask = LayerMask.GetMask(uiLayer);
        }

        protected override void OnDisposed()
        {
            
        }

        
        public bool AddUI(GameObject ui)
        {
            if (!ui.TryGetComponent<IBaseUI>(out var uiRoot))
                return false;

            var uiType = uiRoot.GetType();
            if (uiContainer.ContainsKey(uiType))
                return false;
            
            var uiInfo = new UIInfo(ui, uiRoot);
            if (!uiContainer.TryAdd(uiType, uiInfo)) 
                return false;
            
            uiRoot.OnRefresh();
            return true;
        }

        public bool RemoveUI(GameObject ui)
        {
            if (!ui.TryGetComponent<IBaseUI>(out var uiRoot))
                return false;

            var uiType = uiRoot.GetType();
            if (!uiContainer.TryGetValue(uiType, out var uiInfo)) 
                return false;

            if (uiInfo.UI != null)
            {
                Destroy(uiInfo.UI);
            }

            if (uiInfo.UIHandle.IsValid())
            {
                Addressables.Release(uiInfo.UIHandle);
            }

            return uiContainer.Remove(uiType);
        }
        
        public void ClearUI()
        {
            foreach (var uiInfo in uiContainer.Values)
            {
                if (uiInfo.UI != null)
                {
                    Destroy(uiInfo.UI);    
                }
                
                if (uiInfo.UIHandle.IsValid())
                {
                    Addressables.Release(uiInfo.UIHandle);
                }
            }
            
            uiContainer.Clear();
        }
        
        public T FindUI<T>()
        {
            if (!uiContainer.TryGetValue(typeof(T), out var uiInfo)) 
                return default;

            return (T)uiInfo.UIBase;
        }

        public void ActiveUI<T>(bool active)
        {
            if (!uiContainer.TryGetValue(typeof(T), out var uiInfo)) 
                return;
            
            uiInfo.UI.SetActive(active);
        }
        
        public T OpenUI<T>()
        {
            var uiType = typeof(T);
            if (uiContainer.TryGetValue(uiType, out var uiInfo))
            {
                uiInfo.UI.transform.SetAsLastSibling();
                uiInfo.UIBase.OnRefresh();
                uiInfo.UIBase.OnOpen();
                return (T)uiInfo.UIBase;
            }
            
            var uiHandle = Addressables.InstantiateAsync(uiType.Name, gameObject.transform);
            uiHandle.WaitForCompletion();

            if (uiHandle.Status != AsyncOperationStatus.Succeeded)
                return default;

            if (!uiHandle.Result.TryGetComponent<IBaseUI>(out var uiRoot))
            {
                Addressables.Release(uiHandle);
                return default;
            }
                
            uiInfo = new UIInfo(uiHandle.Result, uiRoot, uiHandle);
            if (uiContainer.TryAdd(uiType, uiInfo))
            {
                uiInfo.UI.transform.SetAsLastSibling();
                uiInfo.UIBase.OnRefresh();
                uiInfo.UIBase.OnOpen();
                return (T)uiInfo.UIBase;
            }
            
            Addressables.Release(uiHandle);
            return default;
        }

        public void CloseUI<T>()
        {
            if (uiContainer.TryGetValue(typeof(T), out var uiInfo))
            {
                uiInfo.UIBase.OnClose();
            }
        }

        public void CloseAllUI()
        {
            foreach (var uiInfo in uiContainer.Values)
            {
                uiInfo.UIBase.OnClose();
            }
        }
        
        public void RefreshUI<T>()
        {
            if (uiContainer.TryGetValue(typeof(T), out var uiInfo))
            {
                uiInfo.UIBase.OnRefresh();
            }
        }
    }
}