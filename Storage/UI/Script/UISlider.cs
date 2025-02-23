using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    // ==================================================
    // Revision and reporting of the script is required.
    // ==================================================
    [AddComponentMenu("UI/UISlider")]
    [RequireComponent(typeof(Slider))]
    public sealed class UISlider : MonoBehaviour
    {
        [SerializeField] private TMP_Text sliderPercentage;
        [SerializeField] private bool sliderWholeNumber;
        [SerializeField] private string sliderSuffix = "%";

        private Slider slider;

        
        public void SetValue(float value)
        {
            if (slider == null)
                return;
            
            slider.value = value;
        }
        
        public void SetPercentage(float value)
        {
            if (sliderPercentage == null)
                return;
            
            sliderPercentage.text = sliderWholeNumber ? $"{(int)value}{sliderSuffix}" : $"{Math.Round(value, 2)}{sliderSuffix}";
        }
        
        
        private void Start()
        {
            if (!TryGetComponent(out Slider component)) 
            {
                enabled = false;
                return;   
            }
            
            slider = component;
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDestroy()
        {
            if (slider == null)
                return;
            
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        
        private void OnSliderValueChanged(float value)
        {
            SetPercentage(value);
        }
    }
   
}