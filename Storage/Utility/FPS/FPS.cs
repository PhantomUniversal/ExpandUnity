using UnityEngine;

namespace FPS
{
    public class FPS : MonoBehaviour
    {
        private float deltaTime;
        private float ms;
        private float fps;
        
        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            ms = deltaTime * 1000.0f;
            fps = 1.0f / deltaTime;
        }

        private void OnGUI()
        {
            int w = Screen.width, h = Screen.height;
            var style = new GUIStyle();
            var rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperRight;
            style.fontSize = 40;
            style.normal.textColor = Color.green;
            var text = $"{ms:0.0} ms ({fps:0.}fps))";
            GUI.Label(rect, text, style);
        }
    }
}