using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

namespace GUIManager
{
    public class GammaSlider : MonoBehaviour
    {
        [SerializeField] PostProcessVolume postProcess;
        [SerializeField] private Slider _ASlider;
        [SerializeField] private float _IncrementMultiplier;

        Bloom bloomLayer = null;

        private void Start()
        {
            postProcess.profile.TryGetSettings(out bloomLayer);
            _ASlider = GetComponent<Slider>();
            _ASlider.value = bloomLayer.intensity.value;

        }

        public void OnValueChanged(float newValue)
        {
            var gammaValue = newValue * _IncrementMultiplier;
            bloomLayer.intensity.value = gammaValue;
            Debug.Log(newValue);
        }
    }
}
