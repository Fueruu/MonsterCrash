using InControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GUIManager
{
    public class GUIObjectManager : MonoBehaviour
    {
        public AGUIObject focusedButton;

        TwoAxisInputControl filteredDirection;
        public InputDevice deviceInput { get; set; }
        private EventSystem eventSystem;

        void Awake()
        {
            filteredDirection = new TwoAxisInputControl();
            filteredDirection.StateThreshold = 0.5f;
        }

        void Update()
        {
            ControllerInput();
        }
		
		public void SetFocusedButton (AGUIObject newButton){
			focusedButton = newButton;
		}

        private void ControllerInput()
        {
            // Use last device which provided input.
            //var inputDevice = InputManager.ActiveDevice;
            deviceInput = InputManager.ActiveDevice;

            if (deviceInput != null)
            {
                filteredDirection.Filter(deviceInput.Direction, Time.deltaTime);
            }

            // Move focus with directional inputs.
            if (filteredDirection.Up.WasPressed)
            {
                MoveFocusTo(focusedButton.up);
            }

            if (filteredDirection.Down.WasPressed)
            {
                MoveFocusTo(focusedButton.down);
            }

            if (filteredDirection.Left.WasPressed)
            {
                MoveFocusTo(focusedButton.left);
                if (focusedButton.slider)
                {
                    focusedButton.slider.value -= 1;
                }
            }

            if (filteredDirection.Right.WasPressed)
            {
                MoveFocusTo(focusedButton.right);
                if (focusedButton.slider)
                {
                    focusedButton.slider.value += 1;
                }
            }

            if (deviceInput.Action1.WasPressed)
            {
                if (focusedButton.button)
                {
                    ExecuteEvents.Execute(focusedButton.button.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
                }
            }

            void MoveFocusTo(AGUIObject newFocusedButton)
            {
                if (newFocusedButton != null)
                {
                    focusedButton = newFocusedButton;
                }
            }
        }
    }
}