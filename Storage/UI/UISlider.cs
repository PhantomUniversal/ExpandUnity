using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    [RequireComponent(typeof(Slider))]
    public sealed class UISlider : MonoBehaviour
    {
        [SerializeField] private Slider Slider;
        [SerializeField] private TMP_Text SliderPercentage;
        [SerializeField] private string SliderSuffix;
        [SerializeField] private bool SliderWholeNumber;
    
        private void Start()
        {
            Slider?.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDestroy()
        {
            Slider?.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            SetPercentage(value);
        }
    
        private void SetPercentage(float value)
        {
            SliderPercentage.text = SliderWholeNumber ? $"{(int)value}{SliderSuffix}" : $"{Math.Round(value, 2)}{SliderSuffix}";
        }
    }
   
}