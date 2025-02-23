using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PhantomEngine
{
    [CreateAssetMenu(menuName = "Phantom/GameData")]
    public class GameData : ScriptableObject
    {
        public List<AssetReferenceGameObject> Prefabs;
    }
}