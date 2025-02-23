using UnityEngine;

namespace PhantomEngine
{
    // Background infinity scroll
    public class UIBackground : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        
        private MeshRenderer meshRenderer;
        private float offset;

        
        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            offset += Time.deltaTime * speed;
            meshRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}