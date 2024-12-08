using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PhantomEngine
{
    [Serializable]
    public sealed class UIInfo
    {
        public GameObject UI;
        public IBaseUI UIBase;
        public AsyncOperationHandle<GameObject> UIHandle;
        
        
        public UIInfo(GameObject ui, IBaseUI uiBase)
        {
            UI = ui;
            UIBase = uiBase;
        }
        
        public UIInfo(GameObject ui, IBaseUI uiBase, AsyncOperationHandle<GameObject> uiHandle)
        {
            UI = ui;
            UIBase = uiBase;
            UIHandle = uiHandle;
        }
    }
}