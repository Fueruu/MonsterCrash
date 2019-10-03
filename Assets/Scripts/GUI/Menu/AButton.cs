using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GUIManager
{
    public class AButton : AGUIObject
    {
        [SerializeField] private Color selectColor;

        void Start()
        {
            button = GetComponent<Button>();
        }

        void Update()
        {
            FocusOnButton();
        }

        private void FocusOnButton()
        {
            // Are we focusing on the button?
            var hasFocus = objectManager.focusedButton == this;

            // Fade alpha in and out depending on focus.
            var color = button.colors;

            selectColor.a = Mathf.MoveTowards(selectColor.a, hasFocus ? 1.0f : 0.5f, Time.unscaledTime * 3.0f);
            color.normalColor = selectColor;

            button.colors = color;
        }
    }
}





// END ME PLS