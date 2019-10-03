using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

namespace GUIManager
{
    public class AGUIObject : MonoBehaviour
    {
        public AGUIObject up = null;
        public AGUIObject down = null;
        public AGUIObject left = null;
        public AGUIObject right = null;

        [HideInInspector] public Button button;
        [HideInInspector] public Slider slider;
        [HideInInspector] public PlayableDirector director;

		[SerializeField] protected GUIObjectManager objectManager;

        void Update()
        {
            FocusOnButton();
        }

        private void FocusOnButton()
        {
            // Are we focusing on the button?
            var hasFocus = transform.parent.GetComponent<GUIObjectManager>().focusedButton == this;
        }
    }

}







// END ME PLS