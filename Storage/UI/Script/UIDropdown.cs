using TMPro;
using UnityEngine;

namespace PhantomEngine
{
    public class UIDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;


        private void Start()
        {
            dropdown.onValueChanged.AddListener(Select);
        }

        private void OnDestroy()
        {
            dropdown.onValueChanged.RemoveListener(Select);
        }


        private void Select(int index)
        {
            Debug.Log($"Select index: {index}");   
            Debug.Log($"Select text: {dropdown.options[index].text}");
        }
    }
}