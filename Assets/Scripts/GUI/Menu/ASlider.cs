using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GUIManager
{
    public class ASlider : AGUIObject
    {
        [SerializeField] private Color selectColor;

        void Start()
        {
            slider = GetComponent<Slider>();
        }

        void Update()
        {
            FocusedOnSlider();
        }

        private void FocusedOnSlider()
        {
            // Are we focusing on the button?
            var hasFocus = objectManager.focusedButton == this;

            // Fade alpha in and out depending on focus.
            var color = slider.colors;

            selectColor.a = Mathf.MoveTowards(selectColor.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f);
            color.normalColor = selectColor;

            slider.colors = color;
        }
    }
}






// END ME PLS