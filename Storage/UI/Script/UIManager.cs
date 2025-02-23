using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomEngine
{
    public sealed class UIManager : GenericSingleton<UIManager>
    {
        private readonly Dictionary<Type, UIInfo> uiContainer = new();
        
        // Addressable / UI
        // Patch
        // PreLoad
        // Get
        // Find
        // Exist
        // Open
        // Close
        // Refresh
        // ...
        
        protected override void OnInitialized()
        {
            
        }

        protected override void OnDisposed()
        {
            
        }

        
        public bool AddUI(GameObject ui)
        {
            if (!ui.TryGetComponent<IBaseUI>(out var uiBase))
                return false;
            
            var uiInfo = new UIInfo(ui, uiBase);
            var uiType = uiBase.GetType();
            if (uiContainer.ContainsKey(uiType))
                return false;
            
            return uiContainer.TryAdd(uiType, uiInfo);
        }

        public bool RemoveUI(GameObject ui)
        {
            if (!ui.TryGetComponent<IBaseUI>(out var uiBase))
                return false;
            
            var uiType = uiBase.GetType();
            return uiContainer.Remove(uiType);
        }
        
        public void ClearUI()
        {
            if (uiContainer.Count == 0)
                return;
            
            uiContainer.Clear();
        }
        
        public T GetUI<T>()
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
            if (!uiContainer.TryGetValue(typeof(T), out var uiInfo))
                return default;
            
            uiInfo.UI.transform.SetAsLastSibling();
            uiInfo.UIBase.OnRefresh();
            uiInfo.UIBase.OnOpen();
            return (T)uiInfo.UIBase;
        }

        public void CloseUI<T>()
        {
            if (!uiContainer.TryGetValue(typeof(T), out var uiInfo))
                return;
            
            uiInfo.UIBase.OnClose();
        }

        public void CloseAllUI()
        {
            if (uiContainer.Count == 0)
                return;
            
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