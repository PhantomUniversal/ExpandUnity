using UnityEngine;

namespace PhantomEngine
{
    public class UIResolution : MonoBehaviour
    {
        [SerializeField] 
        private Camera UICamera;
        [SerializeField] 
        private Vector2 UISize = Vector2.zero;
        [SerializeField]
        private int UIFrame;
        
        
        private void Awake()
        {
            Resolution();
        }


        private void Resolution()
        {
            if (UICamera && UISize is { x: > 0, y: > 0 })
            {
                var screenSize = new Vector2(Screen.width, Screen.height);
                Screen.SetResolution((int)UISize.x,
                    (int)(screenSize.y / screenSize.x * UISize.x), true);

                if (UISize.x / UISize.y < screenSize.x / screenSize.y)
                {
                    var width = UISize.x / UISize.y / (screenSize.x / screenSize.y);
                    UICamera.rect = new Rect((1f - width) / 2f, 0f, width, 1f);
                }
                else
                {
                    var height = screenSize.x / screenSize.y / (UISize.x / UISize.y);
                    UICamera.rect = new Rect(0f, (1f - height) / 2f, 1f, height);
                }
            }

            if (UIFrame > 0) 
                Application.targetFrameRate = UIFrame;
        }
    }
}