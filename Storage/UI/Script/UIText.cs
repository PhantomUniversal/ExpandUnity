using TMPro;
using UnityEngine;

namespace PhantomEngine
{
    [RequireComponent(typeof(TMP_Text))]
    public class UIText : MonoBehaviour
    {
        private TMP_Text text;
        private Color textColor;

        
        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }


        public void SetColor(string colorString)
        {
            if (!colorString.Contains("#"))
            {
                colorString = "#" + colorString;
            }
            
            ColorUtility.TryParseHtmlString(colorString, out textColor);
            text.color = textColor;
        }

        public void RandomColor()
        {
            textColor = Random.ColorHSV();
            text.color = textColor;
            
            // Show color text
            //text.text = ColorUtility.ToHtmlStringRGB(textColor);
        }
    }
}