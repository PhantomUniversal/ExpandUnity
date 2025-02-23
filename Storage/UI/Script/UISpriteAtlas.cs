using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace PhantomEngine
{
    public class UISpriteAtlas : MonoBehaviour
    {
        [SerializeField] private SpriteAtlas atlas;
        [SerializeField] private List<UIItem> atlasItems;


        private void Start()
        {
            Patch();
        }

        private void Patch()
        {
            foreach (var item in atlasItems)
            {
                item.target.sprite = atlas.GetSprite(item.path);
            }
        }
    }
}