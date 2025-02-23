using System;
using UnityEngine;

namespace PhantomEngine
{
    [Serializable]
    public sealed class UIInfo
    {
        public GameObject UI;
        public IBaseUI UIBase;
        
        public UIInfo(GameObject ui, IBaseUI uiBase)
        {
            UI = ui;
            UIBase = uiBase;
        }
    }
}