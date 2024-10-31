using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    [RequireComponent(typeof(Slider))]
    public class UISlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text sliderPercentage;
        [SerializeField] private string sliderSuffix;
        [SerializeField] private bool sliderWholeNumber;
    
        private void Start()
        {
            slider?.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDestroy()
        {
            slider?.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            SetPercentage(value);
        }
    
        private void SetPercentage(float value)
        {
            sliderPercentage.text = sliderWholeNumber ? $"{(int)value}{sliderSuffix}" : $"{Math.Round(value, 2)}{sliderSuffix}";
        }
    }
   
}